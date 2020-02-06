using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using ULA.AddinsHost.Presentation;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Business.Image
{
    /// <summary>
    /// Изображение на фоне
    /// </summary>
    [Serializable]
    public class BackgroundPicture : ViewModelBase, IBackgroundPicture
    {
        private ImageSource _backgroundImageSource;
        private int _zIndexValue;
        private int _canvasTop;
        private int _canvasLeft;
        private int _imageHeight;
        private int _imageWidth;

        private bool _isEditMode;
        private string _imagePath;

        /// <summary>
        /// Изображение на фоне
        /// </summary>
        public BackgroundPicture()
        {
            ImageHeight = 200;
            ImageWidth = 200;
        }


        /// <summary>
        /// Сама картинка
        /// </summary>
        [XmlIgnore]
        public ImageSource BackgroundImageSource
        {
            get { return _backgroundImageSource; }
            set
            {
                _backgroundImageSource = value;
                OnPropertyChanged(nameof(BackgroundImageSource));
            }
        }

        /// <summary>
        /// Путь к файлу картинки
        /// </summary>
        [XmlText]
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        /// <summary>
        /// Положение картинки поверх или под остаными частями приложения
        /// </summary>
        [XmlAttribute]
        public int ZIndexValue
        {
            get { return _zIndexValue; }
            set
            {
                _zIndexValue = value;
                OnPropertyChanged(nameof(ZIndexValue));
            }
        }

        /// <summary>
        /// Показывает находится ли картинка в режиме редактирования (если нет, то ее z-индекс нулевой)
        /// </summary>
        [XmlAttribute]
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                _isEditMode = value;
                if (value) ZIndexValue = 6;
                else
                {
                    ZIndexValue = 0;
                }
                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        /// <summary>
        /// Позиция картинки по верхнему краю канваса
        /// </summary>
        [XmlAttribute]
        public int CanvasTop
        {
            get { return _canvasTop; }
            set
            {
                _canvasTop = value;
                OnPropertyChanged(nameof(CanvasTop));
            }
        }

        /// <summary>
        /// Позиция картинки по левому краю канваса
        /// </summary>
        [XmlAttribute]
        public int CanvasLeft
        {
            get { return _canvasLeft; }
            set
            {
                _canvasLeft = value;
                OnPropertyChanged(nameof(CanvasLeft));
            }
        }



        /// <summary>
        /// Позиция картинки по нижнему краю канваса
        /// </summary>
        [XmlAttribute]
        public int ImageHeight
        {
            get { return _imageHeight; }
            set
            {
                _imageHeight = value;
                OnPropertyChanged(nameof(ImageHeight));
            }
        }

        /// <summary>
        /// Позиция картинки по правому краю канваса
        /// </summary>
        [XmlAttribute]
        public int ImageWidth
        {
            get { return _imageWidth; }
            set
            {
                _imageWidth = value;
                OnPropertyChanged(nameof(ImageWidth));
            }
        }




        /// <summary>
        /// Загрузить картинку
        /// </summary>
        public void Load()
        {
            if (!File.Exists("ImageConfig.xml")) return;
            try
            {
                var serializer = new XmlSerializer(typeof (BackgroundPicture));
                using (var stream = new FileStream("ImageConfig.xml", FileMode.Open, FileAccess.Read))
                {
                    BackgroundPicture bp = serializer.Deserialize(stream) as BackgroundPicture;
                    if (bp.ImagePath != null)
                    {
                        InitImageFromPath(bp.ImagePath);
                    }
                    CanvasLeft = bp.CanvasLeft;
                    CanvasTop = bp.CanvasTop;
                    ImageWidth = bp.ImageWidth;
                    ImageHeight = bp.ImageHeight;
                    ZIndexValue = bp.ZIndexValue;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Сохранить картинку
        /// </summary>
        public void Save(bool deleted)
        {
      
            var serializer = new XmlSerializer(typeof (BackgroundPicture));
            using (var stream = new FileStream("ImageConfig.xml", FileMode.Create, FileAccess.Write))
            {
                if (!deleted)
                {
                      serializer.Serialize(stream, this);
                }
              
            }
        }

        /// <summary>
        /// инициализировать изображение из файла картинки
        /// </summary>
        /// <param name="fileName"></param>
        public void InitImageFromPath(string fileName)
        {
            BackgroundImageSource =
                new BitmapImage(new Uri(fileName, UriKind.Absolute));
            ImagePath = fileName;
        }
    }
}