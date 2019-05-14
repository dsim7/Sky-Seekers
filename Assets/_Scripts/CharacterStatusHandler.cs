﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStatusHandler : ObservedObject
{
    Character owner;
    
    List<StatusEffectInstance> statuses;

    public CharacterStatusHandler(Character owner)
    {
        this.owner = owner;
        statuses = new List<StatusEffectInstance>();
    }

    public void ApplyStatus(StatusEffectTemplate statusTemplate, Character target)
    {
        if (!statusTemplate.Stacks)
        {
            StatusEffectInstance preexistingStatus = target.StatusHandler.FindStatus(statusTemplate);
            if (preexistingStatus != null)
            {
                preexistingStatus.Reset();
                Changed();
                return;
            }
        }

        StatusEffectInstance status = StatusEffectInstance.NewStatus(statusTemplate, owner, target);
        statuses.Add(status);
        status.OnApply();

        Changed();
    }

    public void PerTurnStatuses()
    {
        statuses.ForEach(status => status.PerTurn());
        Changed();
    }

    public void RemoveExpiredStatuses()
    {
        for (int i = statuses.Count - 1; i >= 0; i--)
        {
            StatusEffectInstance status = statuses[i];
            if (status.CurrentDuration <= 0)
            {
                RemoveStatus(status, false);
            }
        }
    }

    public void RemoveStatus(StatusEffectInstance status, bool update = true)
    {
        statuses.Remove(status);
        status.OnRemove();
        if (update)
        {
            Changed();
        }
    }

    public StatusEffectInstance FindStatus(StatusEffectTemplate statusTemplate)
    {
        return statuses.FirstOrDefault(status => status.Template == statusTemplate);
    }
}
