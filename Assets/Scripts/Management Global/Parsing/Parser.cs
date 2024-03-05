using System.Collections.Generic;


public abstract class Parser
{
    public abstract string InTextNewLineCharacter { get; protected set; }
    public abstract string Extension { get; protected set; }
    public abstract bool HasCustomBackground { get; protected set; }
    public abstract string CustomBackground { get; protected set; }
    public abstract bool DirectlyDisplayText { get; protected set; }
    public abstract IEnumerable<Marker> Markers { get; protected set; }
    public abstract bool IsCompatible(GamePath path);
    public abstract bool Parse(GamePath path);
}
