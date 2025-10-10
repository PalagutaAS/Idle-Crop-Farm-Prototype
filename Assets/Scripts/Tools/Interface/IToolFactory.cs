namespace Tools.Interface
{
    public interface IToolFactory
    {
        ITool CreateTool(ToolType type);
    }
}