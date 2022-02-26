namespace Jalindi.VideoUtil.Util;

public enum AspectRatio
{
    Unknown, ThreeTwo, FourThree, FiveFour, SixteenTen, SixteenNine, OnePointEightFive, TwoPointThreeFive, NineSixteen
}

public static class AspectRatioHelper
{
    public static string RatioText(this AspectRatio ratio)
    {
        return ratio switch
        {
            AspectRatio.ThreeTwo => "3:2",
            AspectRatio.FourThree => "4:3",
            AspectRatio.FiveFour => "5:4",
            AspectRatio.SixteenTen => "16:10",
            AspectRatio.SixteenNine => "16:9",
            AspectRatio.OnePointEightFive => "1.85:1",
            AspectRatio.TwoPointThreeFive => "2.35:1",
            AspectRatio.NineSixteen => "9:16",
            AspectRatio.Unknown => "Unknown",
            _ => throw new ArgumentOutOfRangeException(nameof(AspectRatio), ratio, null)
        };
    }
    public static double Ratio(this AspectRatio ratio)
    {
        var dimension = ratio.RatioText().Split(':');
        if (dimension.Length != 2) throw new ArgumentOutOfRangeException(nameof(AspectRatio), ratio, null);
        var ratioValue= double.Parse(dimension[1]) / double.Parse(dimension[0]);
        return  (double)(int)(ratioValue * 1000) / 1000;
    }

    public static AspectRatio GetAspectRatio(int width, int height)
    {
        var ratio = (double)height / (double)width;
        var closestAspectRatio = AspectRatio.ThreeTwo;
        double closestDistance = 1000;
        foreach (var aspectRatioObject in Enum.GetValues(typeof(AspectRatio)))
        {
            var aspectRatio = (AspectRatio)aspectRatioObject;
            if (aspectRatio==AspectRatio.Unknown) continue;
            var distance = Math.Abs(ratio - aspectRatio.Ratio());
            if (!(distance < closestDistance)) continue;
            closestAspectRatio = aspectRatio;
            closestDistance = distance;
        }

        return  closestAspectRatio;
    }

}