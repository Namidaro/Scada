

using System.Windows.Media;

namespace ULA.AddinsHost.Presentation
{
    /// <summary>
    /// изображение на фоне
    /// </summary>
    public interface IBackgroundPicture
    {
        /// <summary>
        /// само изображение
        /// </summary>
        ImageSource BackgroundImageSource { get; set; }

        /// <summary>
        /// путь к изображению
        /// </summary>
        string ImagePath { get; set; }

        /// <summary>
        /// Положение картинки поверх или под остаными частями приложения
        /// </summary>
        int ZIndexValue { get; set; }

        /// <summary>
        /// Показывает находится ли картинка в режиме редактирования
        /// </summary>
        bool IsEditMode { get; set; }

        /// <summary>
        /// Позиция картинки по верхнему краю канваса
        /// </summary>
        int CanvasTop { get; set; }

        /// <summary>
        /// Позиция картинки по левому краю канваса
        /// </summary>
        int CanvasLeft { get; set; }

    /// <summary>
        /// Позиция картинки по нижнему краю канваса
        /// </summary>
        int ImageHeight { get; set; }

        /// <summary>
        /// Позиция картинки по правому краю канваса
        /// </summary>
        int ImageWidth { get; set; }

        /// <summary>
        /// Загрузить картинку
        /// </summary>
        void Load();

        /// <summary>
        /// Сохранить картинку
        /// </summary>
        void Save(bool deleted);
        /// <summary>
        /// инициализировать изображение из файла картинки
        /// </summary>
        /// <param name="fileName"></param>
        void InitImageFromPath(string fileName);
    }
}