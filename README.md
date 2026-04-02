# QR Code Toolbox

A lightweight WPF desktop application for generating QR codes from text or URLs. Built with .NET 10 and IronBarCode.

## Features

- Generate QR codes from any text or URL
- Adjustable module size via slider (30–70 px per module)
- Live preview with high-quality rendering
- Save generated QR codes as PNG files
- Copy QR codes directly to the clipboard
- Automatic dark/light mode that follows your Windows system theme

## Requirements

- Windows 10 or later
- [.NET 10 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

## Getting Started

### Clone and build

```bash
git clone https://github.com/AlbertAssaad/QRCodeToolbox.git
cd QRCodeToolbox
dotnet build
dotnet run
```

### Or run the executable directly

Download the latest release from the [Releases](../../releases) page and run `QRCodeToolbox.exe`.

## Usage

1. Type or paste any text or URL into the input box.
2. Adjust the **Module Size** slider to control the output resolution.
3. Click **Generate** to create the QR code.
4. Use **Save as PNG** to export the image, or **Copy to Clipboard** to paste it elsewhere.
5. Click **Clear** to reset the form.

## Tech Stack

| Component | Technology |
|-----------|------------|
| Framework | .NET 10 WPF |
| QR Generation | [IronBarCode](https://ironsoftware.com/csharp/barcode/) |
| Architecture | MVVM (ViewModel + RelayCommand) |
| Theming | ResourceDictionary (Light / Dark) |

## Project Structure

```
QRCodeToolbox/
├── Models/
│   └── QRCodeModel.cs        # QR code generation and file saving
├── ViewModels/
│   ├── MainViewModel.cs      # Application logic and commands
│   ├── ViewModelBase.cs      # INotifyPropertyChanged base
│   └── RelayCommand.cs       # ICommand implementation
├── Themes/
│   ├── Light.xaml            # Light theme brushes
│   └── Dark.xaml             # Dark theme brushes
├── MainWindow.xaml           # Main UI layout
└── App.xaml.cs               # Theme detection and startup
```

## License

This project is licensed under the MIT License.

> **Note:** IronBarCode is a commercial library. A valid license key is required for production use. Trial licenses are available at [ironsoftware.com](https://ironsoftware.com/csharp/barcode/).
