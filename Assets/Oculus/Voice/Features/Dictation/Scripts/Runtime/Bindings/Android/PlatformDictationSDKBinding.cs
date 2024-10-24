#if UNITY_EDITOR
/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using Oculus.Voice.Core.Bindings.Android;

namespace Oculus.Voice.Dictation.Bindings.Android
{
    public class PlatformDictationSDKBinding : BaseServiceBinding
    {
        public bool Active => binding.Call<bool>("isActive");
        public bool IsRequestActive => binding.Call<bool>("isRequestActive");
        public bool IsSupported => binding.Call<bool>("isSupported");

        public PlatformDictationSDKBinding(AndroidJavaObject sdkInstance) : base(sdkInstance) {}

        public void StartDictation(DictationConfigurationBinding configuration)
        {
            binding.Call("startDictation", configuration.ToJavaObject());
        }

        public void StopDictation()
        {
            binding.Call("stopDictation");
        }

        public void SetListener(DictationListenerBinding listenerBinding)
        {
            binding.Call("setListener", listenerBinding);
        }
    }
}
#endif