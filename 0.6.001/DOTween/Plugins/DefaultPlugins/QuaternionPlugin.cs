﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2014/07/07 20:02
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 

using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using UnityEngine;

#pragma warning disable 1591
namespace DG.Tweening.Plugins.DefaultPlugins
{
    public class QuaternionPlugin : ABSTweenPlugin<Quaternion>
    {
        Vector3 _res;

        public override void SetStartValue(TweenerCore<Quaternion> t)
        {
            t.startValueV4 = GetTargetValue(t).eulerAngles;
        }

        public override void Evaluate(TweenerCore<Quaternion> t, float elapsed)
        {
            _res.x = Ease.Apply(t, elapsed, t.startValueV4.x, t.changeValueV4.x, t.duration, 0, 0);
            _res.y = Ease.Apply(t, elapsed, t.startValueV4.y, t.changeValueV4.y, t.duration, 0, 0);
            _res.z = Ease.Apply(t, elapsed, t.startValueV4.z, t.changeValueV4.z, t.duration, 0, 0);
            
            // Apply to eventual known type
            switch (t.targetType) {
            case TargetType.TransformRotation:
                t.targetTransform.eulerAngles = _res;
                break;
            case TargetType.TransformLocalRotation:
                t.targetTransform.localEulerAngles = _res;
                break;
            default:
                t.setter(Quaternion.Euler(_res));
                break;
            }
        }

        static Quaternion GetTargetValue(TweenerCore<Quaternion> t)
        {
            switch (t.targetType) {
            case TargetType.TransformRotation:
                return t.targetTransform.rotation;
            case TargetType.TransformLocalRotation:
                return t.targetTransform.localRotation;
            }
            return t.getter();
        }
    }
}