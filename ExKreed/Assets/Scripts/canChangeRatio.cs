using System;

public interface canChangeRatio
{
    Action<int,int> onChangeEvent { get; set; }
    float getMax();
    float getCur();
    float getRatio();
}
