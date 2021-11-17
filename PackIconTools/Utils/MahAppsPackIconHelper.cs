using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PackIconTools.Utils
{
    public static class MahAppsPackIconHelper
    {

        public static string GetPathData(object iconKind)
        {
            string value = null;
            if (iconKind is PackIconBootstrapIconsKind botstrapIconsKind)
            {
                PackIconBootstrapIconsDataFactory.DataIndex.Value?.TryGetValue(botstrapIconsKind, out value);
            }
            else if (iconKind is PackIconCodiconsKind codiconsKind)
            {
                PackIconCodiconsDataFactory.DataIndex.Value?.TryGetValue(codiconsKind, out value);
            }
            else if (iconKind is PackIconFileIconsKind fileIconsKind)
            {
                PackIconFileIconsDataFactory.DataIndex.Value?.TryGetValue(fileIconsKind, out value);
            }
            else if (iconKind is PackIconFontaudioKind fontaudioKind)
            {
                PackIconFontaudioDataFactory.DataIndex.Value?.TryGetValue(fontaudioKind, out value);
            }
            else if (iconKind is PackIconForkAwesomeKind forkAwesomKind)
            {
                PackIconForkAwesomeDataFactory.DataIndex.Value?.TryGetValue(forkAwesomKind, out value);
            }
            else if (iconKind is PackIconMicronsKind micronsKind)
            {
                PackIconMicronsDataFactory.DataIndex.Value?.TryGetValue(micronsKind, out value);
            }
            else if (iconKind is PackIconVaadinIconsKind vaadinIconsKind)
            {
                PackIconVaadinIconsDataFactory.DataIndex.Value?.TryGetValue(vaadinIconsKind, out value);
            }
            else if (iconKind is PackIconBoxIconsKind)
            {
                PackIconBoxIconsKind key = (PackIconBoxIconsKind)iconKind;
                PackIconBoxIconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconEntypoKind)
            {
                PackIconEntypoKind key = (PackIconEntypoKind)iconKind;
                PackIconEntypoDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconEvaIconsKind)
            {
                PackIconEvaIconsKind key = (PackIconEvaIconsKind)iconKind;
                PackIconEvaIconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconFeatherIconsKind)
            {
                PackIconFeatherIconsKind key = (PackIconFeatherIconsKind)iconKind;
                PackIconFeatherIconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconFontAwesomeKind)
            {
                PackIconFontAwesomeKind key = (PackIconFontAwesomeKind)iconKind;
                PackIconFontAwesomeDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconIoniconsKind)
            {
                PackIconIoniconsKind key = (PackIconIoniconsKind)iconKind;
                PackIconIoniconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconJamIconsKind)
            {
                PackIconJamIconsKind key = (PackIconJamIconsKind)iconKind;
                PackIconJamIconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconMaterialDesignKind)
            {
                PackIconMaterialDesignKind key = (PackIconMaterialDesignKind)iconKind;
                PackIconMaterialDesignDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconMaterialKind)
            {
                PackIconMaterialKind key = (PackIconMaterialKind)iconKind;
                PackIconMaterialDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconMaterialLightKind)
            {
                PackIconMaterialLightKind key = (PackIconMaterialLightKind)iconKind;
                PackIconMaterialLightDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconModernKind)
            {
                PackIconModernKind key = (PackIconModernKind)iconKind;
                PackIconModernDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconOcticonsKind)
            {
                PackIconOcticonsKind key = (PackIconOcticonsKind)iconKind;
                PackIconOcticonsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconPicolIconsKind)
            {
                PackIconPicolIconsKind key = (PackIconPicolIconsKind)iconKind;
                PackIconPicolIconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconRPGAwesomeKind)
            {
                PackIconRPGAwesomeKind key = (PackIconRPGAwesomeKind)iconKind;
                PackIconRPGAwesomeDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconSimpleIconsKind)
            {
                PackIconSimpleIconsKind key = (PackIconSimpleIconsKind)iconKind;
                PackIconSimpleIconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconTypiconsKind)
            {
                PackIconTypiconsKind key = (PackIconTypiconsKind)iconKind;
                PackIconTypiconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconUniconsKind)
            {
                PackIconUniconsKind key = (PackIconUniconsKind)iconKind;
                PackIconUniconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconWeatherIconsKind)
            {
                PackIconWeatherIconsKind key = (PackIconWeatherIconsKind)iconKind;
                PackIconWeatherIconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconZondiconsKind)
            {
                PackIconZondiconsKind key = (PackIconZondiconsKind)iconKind;
                PackIconZondiconsDataFactory.DataIndex.Value?.TryGetValue(key, out value);
            }
            else if (iconKind is PackIconRadixIconsKind redix)
            {
                PackIconRadixIconsDataFactory.DataIndex.Value?.TryGetValue(redix, out value);
            }
            else if (iconKind is PackIconPixelartIconsKind pixelart)
                PackIconPixelartIconsDataFactory.DataIndex.Value?.TryGetValue(pixelart, out value);
            else if (iconKind is PackIconRemixIconKind remix)
                PackIconRemixIconDataFactory.DataIndex.Value?.TryGetValue(remix, out value);

            return value;
        }


        public static DrawingGroup GetDrawingGroup(object iconKind, Brush foregroundBrush, string path)
        {
            GeometryDrawing value = new GeometryDrawing
            {
                Geometry = Geometry.Parse(path),
                Brush = foregroundBrush
            };
            return new DrawingGroup
            {
                Children =
                {
                    (System.Windows.Media.Drawing)value
                },
                Transform = GetTransformGroup(iconKind)
            };
        }

        //
        // 摘要:
        //     Gets the ImageSource for the given kind.
        public static ImageSource CreateImageSource(object iconKind, Brush foregroundBrush)
        {
            string pathData = GetPathData(iconKind);
            if (string.IsNullOrEmpty(pathData))
            {
                return null;
            }

            DrawingImage drawingImage = new DrawingImage(GetDrawingGroup(iconKind, foregroundBrush, pathData));
            drawingImage.Freeze();
            return drawingImage;
        }

        public static Transform GetTransformGroup(object iconKind)
        {
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(GetScaleTransform(iconKind));
            transformGroup.Children.Add(new ScaleTransform(1, 1));
            transformGroup.Children.Add(new RotateTransform());
            return transformGroup;
        }

        public static ScaleTransform GetScaleTransform(object iconKind)
        {
            return new ScaleTransform(1.0, 1.0);
        }
    }
}
