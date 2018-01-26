namespace DMP.Animation
{
    /// <summary>
    /// Styles of page animations for appearing and disappearing
    /// </summary>
    public enum PageAnimation
    {
        /// <summary>
        /// No animation, simples!
        /// </summary>
        None = 0,

        /// <summary>
        /// Page slide in and fades in from the right
        /// </summary>
        SlideAndFadeInFromRight = 1,

        /// <summary>
        /// The page slide out and fades out to the left
        /// </summary>
        SlideAndFadeOutToLeft = 2,

        /// <summary>
        /// The page slides in and fades in from the left
        /// </summary>
        SlideAndFadeInFromLeft = 3,

        /// <summary>
        /// The page slides out and fades out to the right
        /// </summary>
        SlideAndFadeOutToRight = 4
    }
}