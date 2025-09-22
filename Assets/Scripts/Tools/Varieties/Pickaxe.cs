namespace Tools.Varieties
{
    public class Pickaxe : Tool
    {
        private void Update()
        {
            FollowToSlot();
            CropDetecting();
        }
    }
}