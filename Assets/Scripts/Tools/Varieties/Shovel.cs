namespace Tools.Varieties
{
    public class Shovel : Tool
    {
        private void Update()
        {
            FollowToSlot();
            CropDetecting();
        }
    }
}