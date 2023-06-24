using System.Drawing;

namespace DynamicBayesNetNavigation.ProblemRepresentation.Utils
{
    public static class ColorExtension
    {
        public static uint ToARGB(this Color color)
        {
            return BitConverter.ToUInt32(new[] { color.A, color.R, color.G, color.B }, 0);
        }

        public static uint GetHashForColorCollection(this IReadOnlyCollection<uint> collection) =>
            collection.Aggregate<uint, uint>(0, (current, element) => current | element);
    }
}
