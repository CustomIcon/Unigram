﻿#pragma once

#include "VoipVideoOutputSink.g.h"

#include "VoipVideoOutput.h"

#include "api/video/video_sink_interface.h"
#include "api/video/video_frame.h"

using namespace winrt::Telegram::Td::Api;
using namespace winrt::Windows::Foundation::Collections;
using namespace winrt::Windows::UI::Composition;

namespace winrt::Telegram::Native::Calls::implementation
{
    struct VoipVideoOutputSink : VoipVideoOutputSinkT<VoipVideoOutputSink>
    {
        VoipVideoOutputSink(SpriteVisual visual, bool mirrored);
        VoipVideoOutputSink(SpriteVisual visual, bool mirrored, hstring endpointId, winrt::guid visualId);

        bool IsMirrored();
        void IsMirrored(bool value);

        int32_t PixelWidth();
        int32_t PixelHeight();

        std::shared_ptr<VoipVideoOutput> Sink();

        winrt::event_token FrameReceived(Windows::Foundation::TypedEventHandler<
            winrt::Telegram::Native::Calls::VoipVideoOutputSink,
            winrt::Telegram::Native::Calls::FrameReceivedEventArgs> const& value);
        void FrameReceived(winrt::event_token const& token);

        bool Matches(hstring endpointId, winrt::guid visualId);

        void Stop();

    private:
        std::shared_ptr<VoipVideoOutput> m_sink;
        winrt::guid m_visualId;
        hstring m_endpointId;
    };
}

namespace winrt::Telegram::Native::Calls::factory_implementation
{
    struct VoipVideoOutputSink : VoipVideoOutputSinkT<VoipVideoOutputSink, implementation::VoipVideoOutputSink>
    {
    };
} // namespace winrt::Telegram::Native::Calls::factory_implementation
