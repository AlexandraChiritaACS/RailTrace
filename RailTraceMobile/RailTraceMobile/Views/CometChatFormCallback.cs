using System;

namespace RailTraceMobile.Views
{
    public interface CometChatFormCallback
    {
        void SuccessCallback(String username);
        void FailCallback(String jSONObject);
    }
}
