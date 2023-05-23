using DataProcess;
using HD.FrameworkDesign;

public class GameContainer : Architecture<GameContainer>
{    
    protected override void Init()
    {
        Register(new ExcelReadWrite());
        Register(new DataManager());
        Register(new UnityData());
        Register(new ObjectPoolGroup());
    }
}
