﻿#if UNITY_EDITOR
/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using System;
using Oculus.Voice.Dictation.Configuration;
using UnityEngine;

namespace Facebook.WitAi.Configuration
{
    [Serializable]
    public class WitDictationRuntimeConfiguration : WitRuntimeConfiguration
    {
        [Header("Dictation")]
        [SerializeField] public DictationConfiguration dictationConfiguration;

        protected override Vector2 RecordingTimeRange => new Vector2(-1, 300);
    }
}
#endif