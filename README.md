<div align="center">

<img src="src/LiveCaptions-Transcriber.ico" width="128" height="128" alt="LiveCaptions-Transcriber Icon"/>

# LiveCaptions Transcriber

### *Enhanced real-time speech transcription tool based on Windows LiveCaptions*

[![Windows 11](https://img.shields.io/badge/platform-Windows11-blue?logo=windows11&style=&color=1E9BFA)](https://www.microsoft.com/en-us/software-download/windows11)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0+-512BD4?style=flat&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/8.0)

**Based on:** [LiveCaptions-Translator](https://github.com/SakiRinn/LiveCaptions-Translator) by [@SakiRinn](https://github.com/SakiRinn)

</div>

## Overview

**✨ LiveCaptions Transcriber = Windows LiveCaptions + Enhanced UI + Continuous Text Display ✨**

A focused transcription tool that enhances Windows LiveCaptions with a modern interface, continuous text accumulation, and easy copy/clear functionality. Perfect for meetings, lectures, gaming, and accessibility needs.

**Key improvements over standard LiveCaptions:**
- **Continuous text display** - See all transcribed text in one scrollable view
- **Copy & Clear functionality** - Easy text management with one-click actions  
- **Overlay mode** - Transparent window for gaming and streaming
- **History logging** - Keep records of all transcriptions
- **Modern UI** - Clean, accessible interface with dark/light theme support

**🚀 Quick Start:** Build from source or download releases when available!

## Use Cases

**📝 Meeting & Lecture Notes**
- Capture spoken content in real-time
- Copy transcriptions for documentation
- Review history for missed details

**🎮 Gaming & Streaming**  
- Overlay transcriptions on games/videos
- Accessibility for hearing-impaired viewers
- Content creation assistance

**♿ Accessibility**
- Enhanced LiveCaptions experience
- Better text visibility and management
- Persistent transcription history

**📚 Content Creation**
- Transcribe interviews and recordings
- Generate subtitles and captions
- Document spoken presentations

## Features

- **🔄 Seamless LiveCaptions Integration**

  Automatically launches and manages Windows LiveCaptions in the background. The original LiveCaptions window is hidden while this tool provides an enhanced interface.

  > ⚠️ **IMPORTANT:** Enable ***Include microphone audio*** in Windows LiveCaptions settings for speech transcription!

- **📝 Continuous Text Display**

  Unlike standard LiveCaptions that shows only current text, this tool accumulates all transcribed text in a scrollable view. Perfect for capturing entire conversations, meetings, or lectures.

- **🎨 Modern Interface**

  Clean Fluent UI design that automatically switches between light and dark themes based on your system settings. Larger, more readable text with better spacing and organization.

- **📋 Copy & Clear Functions**

  - **Copy All**: One-click copying of entire transcription to clipboard
  - **Clear**: Remove current transcription (moves to history)
  - **Click-to-Copy**: Click any text segment to copy it individually

- **🪟 Enhanced Overlay Window**

  Transparent overlay perfect for gaming, streaming, or watching videos. Features:
  - Full transcription display (not just current sentence)
  - Copy/Clear buttons on hover
  - Customizable font size, color, and opacity
  - Resizable and movable
  - Click-through mode available

- **📚 History Management**

  - Automatic logging of all transcriptions with timestamps
  - Export history to CSV files
  - Search and filter capabilities
  - Persistent storage across sessions

- **⚙️ Simple Controls**

  - Always-on-top window option
  - Overlay mode toggle
  - Clean, distraction-free interface
  - No complex configuration needed


## Prerequisites

<div align="center">

| Requirement                                                                                                           | Details                                     |
|-----------------------------------------------------------------------------------------------------------------------|---------------------------------------------|
| <img src="https://img.shields.io/badge/Windows-11%20(22H2+)-0078D6?style=for-the-badge&logo=windows&logoColor=white"> | With LiveCaptions support.                  |
| <img src="https://img.shields.io/badge/.NET-8.0+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">             | Recommended. Not test in previous versions. |

</div>

This tool is based on Windows LiveCaptions, which is available since **Windows 11 22H2**.

We suggest you have **.NET runtime 8.0** or higher installed. If you are not available to install one, you can download the ***with runtime*** version but its size is bigger.

## Getting Started

> ⚠️ **IMPORTANT:** You must complete the following steps before running LiveCaptions Transcriber for the first time.
>
> For detailed information, see Microsoft's guide on [Using live captions](https://support.microsoft.com/en-us/windows/use-live-captions-to-better-understand-audio-b52da59c-14b8-4031-aeeb-f6a47e6055df).

### Step 1: Verify Windows LiveCaptions Availability

Confirm LiveCaptions is available on your system using any of these methods:

- Toggle **Live captions** in the quick settings
- Press **Win + Ctrl + L**
- Access via **Quick settings** > **Accessibility** > **Live captions**
- Open **Start** > **All apps** > **Accessibility** > **Live captions**
- Navigate to **Settings** > **Accessibility** > **Captions** and enable **Live captions**

### Step 2: Configure LiveCaptions

When you first start, Windows LiveCaptions will ask for your consent to process voice data on your device and prompt you to download language files to be used by on-device speech recognition.

After launching Windows LiveCaptions, click the **⚙️ gear** icon to open the setting menu, then select **Position** > **Overlaid on screen**.

> ⚠️ **VERY IMPORTANT!** Otherwise, a display bug will occur on the screen after hiding Windows LiveCaptions.

<div align="center">
  <img src="images/speech_recognition.png" alt="Items under speech recognition" width="80%" />
  <br>
  <em style="font-size:80%">Required speech recognition downloads</em>
  <br>
</div>

After configuration, close Windows LiveCaptions and launch LiveCaptions Transcriber to start using it! 🎉

## Building from Source

1. **Prerequisites:**
   - Windows 11 (22H2 or later)
   - .NET 8.0 SDK or later
   - Visual Studio 2022 or VS Code

2. **Clone and Build:**
   ```bash
   git clone https://github.com/darshankparmar/LiveCaptions-Transcriber.git
   cd LiveCaptions-Transcriber
   dotnet build
   dotnet run
   ```

3. **Create Release Build:**
   ```bash
   dotnet publish -c Release -r win-x64 --self-contained false
   ```

## How It Works

1. **Launch**: The app automatically starts Windows LiveCaptions in the background
2. **Transcribe**: Speak or play audio - text appears in real-time
3. **Accumulate**: All transcribed text builds up in the main window
4. **Manage**: Use Copy/Clear buttons to manage your transcriptions
5. **History**: All sessions are automatically saved for later review

## Interface Overview

- **Current Sentence** (top, blue): Shows what's being transcribed right now
- **Copy/Clear Buttons** (middle): Manage your transcription text
- **Full Transcription** (bottom): Complete scrollable text of everything transcribed
- **Overlay Window**: Floating transparent window for other applications

## Credits

This project is based on the excellent work by [@SakiRinn](https://github.com/SakiRinn) in the [LiveCaptions-Translator](https://github.com/SakiRinn/LiveCaptions-Translator) repository. 

**Modifications made:**
- Removed translation functionality to focus purely on transcription
- Enhanced UI with continuous text display and copy/clear features
- Simplified interface and removed unnecessary configuration options
- Improved overlay window functionality for better user experience

## License

This project maintains the same license as the original LiveCaptions-Translator project.
