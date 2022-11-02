using HOA.Tokens;
using HOA.Map;
using HOA.Players;


public abstract class Request {
	public Source source;
	public virtual void Grant() {}
}
public abstract class RCellSelect : Request {public Cell cell;}
public abstract class RInstanceSelect : Request {public Token instance;}

