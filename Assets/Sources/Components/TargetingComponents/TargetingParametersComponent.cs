﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitas;
using UnityEngine;

[Game]
public sealed class TargetingParametersComponent : IComponent
{
    public bool angularTargeting;
    public float radius;
    public bool onlyPlayerTargeting;
}