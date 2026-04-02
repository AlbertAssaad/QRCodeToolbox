using QRCodeToolbox.Models;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using Microsoft.Win32;

namespace QRCodeToolbox.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string? _inputText;
        private BitmapImage? _qrCodeImage;
        private string? _statusMessage;
        private int _pixelsPerModule = 70;
        private readonly QRCodeModel _qrCodeModel;

        public string? InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        public BitmapImage? QRCodeImage
        {
            get => _qrCodeImage;
            set => SetProperty(ref _qrCodeImage, value);
        }

        public string? StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public int PixelsPerModule
        {
            get => _pixelsPerModule;
            set
            {
                if (value > 0 && value <= 160)
                {
                    SetProperty(ref _pixelsPerModule, value);
                }
            }
        }

        public ICommand GenerateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand ClearCommand { get; }

        public MainViewModel()
        {
            _qrCodeModel = new QRCodeModel();

            GenerateCommand = new RelayCommand(
                _ => GenerateQRCode(),
                _ => !string.IsNullOrWhiteSpace(InputText)
            );

            SaveCommand = new RelayCommand(
                _ => SaveQRCode(),
                _ => QRCodeImage != null
            );

            CopyCommand = new RelayCommand(
                _ => CopyToClipboard(),
                _ => QRCodeImage != null
            );

            ClearCommand = new RelayCommand(
                _ => Clear()
            );
        }

        private void GenerateQRCode()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(InputText))
                {
                    StatusMessage = "Please enter text to encode";
                    return;
                }

                _qrCodeModel.Text = InputText;
                _qrCodeModel.PixelsPerModule = PixelsPerModule;
                QRCodeImage = _qrCodeModel.GenerateQRCode();
                StatusMessage = "QR Code generated successfully";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                QRCodeImage = null;
            }
        }

        private void SaveQRCode()
        {
            try
            {
                if (QRCodeImage == null || string.IsNullOrWhiteSpace(InputText))
                {
                    StatusMessage = "No QR code to save";
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PNG Images (*.png)|*.png|All Files (*.*)|*.*",
                    FileName = "QRCode.png",
                    Title = "Save QR Code"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    _qrCodeModel.Text = InputText;
                    _qrCodeModel.PixelsPerModule = PixelsPerModule;
                    _qrCodeModel.SaveQRCodeToFile(saveFileDialog.FileName);
                    StatusMessage = $"QR Code saved to {saveFileDialog.FileName}";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Save Error: {ex.Message}";
            }
        }

        private void CopyToClipboard()
        {
            try
            {
                if (QRCodeImage == null)
                {
                    StatusMessage = "No QR code to copy";
                    return;
                }

                Clipboard.SetImage(QRCodeImage);
                StatusMessage = "QR Code copied to clipboard";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Copy Error: {ex.Message}";
            }
        }

        private void Clear()
        {
            InputText = string.Empty;
            QRCodeImage = null;
            StatusMessage = "Cleared";
        }
    }
}
