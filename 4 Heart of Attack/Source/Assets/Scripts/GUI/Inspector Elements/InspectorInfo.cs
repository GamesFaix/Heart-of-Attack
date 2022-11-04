using UnityEngine;
using System;

namespace HOA { 

    public static class InspectorInfo {

        public static void Price(Price price, Panel panel)
        {
            Rect box = panel.IconBox;
            if (GUI.Button(box, ""))
                if (GUIInspector.RightClick) 
                    TipInspector.Inspect(ETip.AP);
            GUI.Box(box, Icons.Stats[Stats.Energy], panel.s);
            box = panel.IconBox;
            GUI.Label(box, price.Energy + "", panel.s);
            panel.NudgeX();

            box = panel.IconBox;
            if (GUI.Button(box, ""))
                if (GUIInspector.RightClick) 
                    TipInspector.Inspect(ETip.FP);
            GUI.Box(box, Icons.Stats[Stats.Focus], panel.s);
            box = panel.IconBox;
            GUI.Label(box, price.Focus + "", panel.s);
        }

        public static void Health(Health health, Panel p)
        {
            health.HP.Draw(new Panel(p.Box(p.IconSize + 95), p.LineH, p.s, p.IconSize));
            Rect defBox = p.Box(p.IconSize * 2 + 5);
            if (health.DEF > 0) health.DEF.Draw( new Panel(defBox, p.LineH, p.s, p.IconSize) );
        }
        public static void HealthDEFCap(HealthDEFCap health, Panel p)
        {
            health.HP.Draw(new Panel(p.Box(p.IconSize + 95), p.LineH, p.s, p.IconSize));
            Rect box = p.Box(p.IconSize * 2 + 5);

            if (health.DEF > 0) health.DEF.Draw(new Panel(box, p.LineH, p.s, p.IconSize)); 

            p.NudgeX(); p.NudgeX();
            GUI.Label(p.Box(30), "(Max");
            GUI.Box(p.Box(20), Icons.Stats[Stats.Defense], p.s);
            GUI.Label(p.Box(40), "= " + health.cap + ")");
        }
        public static void HealthHalfDodge(HealthHalfDodge health, Panel p)
        {
            health.HP.Draw(new Panel(p.Box(p.IconSize + 95), p.LineH, p.s, p.IconSize));
            Rect defBox = p.Box(p.IconSize * 2 + 5);

            if (health.DEF > 0) health.DEF.Draw(new Panel(defBox, p.LineH, p.s, p.IconSize));

            p.NudgeX(); p.NudgeX(); p.NudgeY();
            GUI.Label(p.Box(200), "50% chance of taking no damage.");
        }

        public static void Wallet(Wallet wallet, Panel panel)
        {
            wallet.AP.Draw(new Panel(panel.Box(panel.IconSize * 2 + 5), panel.LineH, panel.s, panel.IconSize));
            wallet.FP.Draw(new Panel(panel.Box(panel.IconSize * 2 + 5), panel.LineH, panel.s, panel.IconSize));
        }
        public static void WalletDEF(WalletDEF wallet, Panel p)
        {
            wallet.AP.Draw(new Panel(p.Box(p.IconSize * 2 + 5), p.LineH, p.s, p.IconSize));
            wallet.FP.Draw(new Panel(p.Box(p.IconSize * 2 + 5), p.LineH, p.s, p.IconSize));

            p.NudgeX(); p.NudgeX();
            GUI.Box(p.Box(20), Icons.Stats[Stats.Defense], p.s);
            GUI.Label(p.Box(40), "+1 per ");
            GUI.Box(p.Box(20), Icons.Stats[Stats.Focus], p.s);
            p.NudgeX();
            GUI.Label(p.Box(60), "(Max +" + wallet.cap + ")");
        }
        public static void WalletIN(WalletIN wallet, Panel p)
        {
            wallet.AP.Draw(new Panel(p.Box(p.IconSize * 2 + 5), p.LineH, p.s, p.IconSize));
            wallet.FP.Draw(new Panel(p.Box(p.IconSize * 2 + 5), p.LineH, p.s, p.IconSize));

            p.NudgeX(); p.NudgeX();
            GUI.Box(p.Box(20), Icons.Stats[Stats.Initiative], p.s);
            GUI.Label(p.Box(40), "+1 per ");
            GUI.Box(p.Box(20), Icons.Stats[Stats.Focus], p.s);
        }

        public static void Watch(Watch watch, Panel p)
        {
            watch.IN.Draw(new Panel(p.Box(p.IconSize * 2 + 5), p.LineH, p.s, p.IconSize));

            float x3 = p.x2;

            Rect box;

            if (watch.IsStunned())
            {
                x3 = p.x2;
                Rect stunBox = p.Box(p.IconSize * 2 + 5);
                if (GUI.Button(stunBox, ""))
                {
                    if (GUIInspector.RightClick) { TipInspector.Inspect(ETip.STUN); }
                }
                p.x2 = x3;
                p.NudgeX();
                box = p.Box(p.IconSize);
                GUI.Box(p.Box(p.IconSize), Icons.Stats[Stats.Stun], p.s);
                p.NudgeX();
                p.NudgeY();
                box = p.Box(p.IconSize);
                GUI.Label(p.Box(p.IconSize), watch.STUN + "", p.s);
                p.NudgeY(false);
            }
            else if (watch.IsSkipped())
            {
                p.NudgeX();
                box = p.Box(p.IconSize);
                if (GUI.Button(box, ""))
                {
                    if (GUIInspector.RightClick) { TipInspector.Inspect(ETip.SKIP); }
                }
                GUI.Box(box, Icons.SKIP(), p.s);
            }
        }

        public static void Plane(Plane plane, Panel panel)
        {
            Plane[] planes = new Plane[4] { HOA.Plane.Sunken, HOA.Plane.Ground, HOA.Plane.Air, HOA.Plane.Ethereal };
            foreach (Plane p in planes)
            {
                if ((p & plane) != HOA.Plane.None)
                {
                    Rect box = panel.Box(panel.LineH);
                    if (GUI.Button(box, ""))
                    {
                        //if (GUIInspector.RightClick) {
                        TipInspector.Inspect(ETip.PLANE);
                        //}
                    }
                    GUI.Box(box, Icons.Planes[p], panel.s);
                    panel.NudgeX();
                }
            }
        }
        public static void Sensor(Sensor sensor, Panel p)
        {
            p.NudgeX();
            Rect box = p.IconBox;
            if (GUI.Button(box, "")) { TipInspector.Inspect(ETip.SENSOR); }
            GUI.Box(box, Icons.SENSOR(), p.s);
            p.NudgeX();
            p.NudgeX();
            GUI.Box(p.Box(0.9f), sensor.Parent.ID.FullName, p.s);
            p.NextLine();
            p.NudgeX();
            p.NudgeX();
            GUI.Label(p.TallBox(0.9f, 3), sensor.Desc());
        }
        public static void Timer(Timer timer, Panel p)
        {
            Rect box = p.IconBox;

            if (GUI.Button(box, "")) { TipInspector.Inspect(ETip.TIMER); }
            GUI.Box(box, Icons.TIMER(), p.s);

            p.NudgeY();
            GUI.Label(p.Box(100), timer.Name);
            p.NudgeY(false);

            p.NudgeX();
            p.NudgeY();
            GUI.Label(p.Box(250), timer.Desc());
        }

        public static void Stat(Stat stat, Panel p)
        {
            if (stat.Stats == Stats.Health) { HP(stat, p); return; }
           // if (GUI.Button(p.FullBox, "")) { TipInspector.Inspect(stat.ETip); }
            GUI.Box(p.Box(p.IconSize), Icons.Stats[stat.Stats], p.s);
            p.NudgeX();
            p.NudgeY();

            Color normColor = p.s.normal.textColor;
            if (stat.Modified() > 0) { p.s.normal.textColor = Color.green; }
            else if (stat.Modified() < 0) { p.s.normal.textColor = Color.red; }
            GUI.Label(p.Box(p.IconSize), stat.ToString(), p.s);
            p.s.normal.textColor = normColor;

        }
        public static void HP(Stat stat, Panel p)
        {
            //if (GUI.Button(p.FullBox, "")) { TipInspector.Inspect(stat.ETip); }

            GUI.Box(p.Box(p.IconSize), Icons.Stats[stat.Stats], p.s);
            p.NudgeX();
            p.NudgeY();


            GUI.Label(p.Box(7), "(", p.s);

            Color normColor = p.s.normal.textColor;
            if (stat.Modified() > 0) 
                p.s.normal.textColor = Color.green; 
            else if (stat.Modified() < 0) 
                p.s.normal.textColor = Color.red; 
            GUI.Label(p.Box(p.IconSize), stat.Current.ToString(), p.s);
            p.s.normal.textColor = normColor;

            GUI.Label(p.Box(7), "/", p.s);

            if (stat.MaxModified() > 0)  
                p.s.normal.textColor = Color.green; 
            else if (stat.MaxModified() < 0) 
                p.s.normal.textColor = Color.red; 
            GUI.Label(p.Box(p.IconSize), stat.Max.ToString(), p.s);
            p.s.normal.textColor = normColor;

            GUI.Label(p.Box(7), ")", p.s);
        }

        public static void Ability(Ability a, Panel p)
        {
            GUI.Label(p.LineBox, a.Name, p.s);
            a.Price.Draw(new Panel(p.Box(150), p.LineH, p.s));
            if (a.Used) { GUI.Label(p.Box(150), "Used this turn."); }
            p.NextLine();
            float descH = (p.H - (p.LineH * 2)) / p.H;
            a.DrawAims(new Panel(p.TallWideBox(descH), p.LineH, p.s));
        }

        public static void Aim (Aim a, Panel p) 
        {
            float iconSize = p.LineH;

			Rect iconBox = p.Box(iconSize);
			if (Icons.Trajectories[a.Trajectory] != null) {
				if (GUI.Button(iconBox, "")) {
					//if (GUIInspector.RightClick) {
						TipInspector.Inspect(Tip.Trajectory(a.Trajectory));
					//}
				}
				GUI.Box(iconBox, Icons.Trajectories[a.Trajectory]);
			}
			if (a.RangeString != "") {
				GUI.Label(p.Box(iconSize), a.RangeString, p.s);
			}
			p.NudgeX();
			if (a.Filter != TargetFilter.None) {a.Filter.Display(new Panel(new Rect(p.x2, p.y2, 200, p.LineH), p.LineH, p.s));}
			/*if (TargetIcon != default(Texture2D[])) {
				foreach (Texture2D tex in TargetIcon) {
					GUI.Box(p.Box(iconSize), tex, p.s);
				}
			}
			*
             */
        }

        public static void Body(Body body, Panel p)
        {
            GUI.Label(p.FullBox, "Body.Draw not finished.");
        
        }

        public static void TokenID(TokenID tokenID, Panel p)
        {
            GUI.Label(p.FullBox, "TokenID.Draw not finished.");

        }

        public static void InspectTemplateButton(Token t, Panel p)
        {
            if (GUI.Button(p.FullBox, ""))
                if (GUIInspector.LeftClick)
                    GUIInspector.Inspected = t.Template();
            GUI.Box(p.Box(p.LineH), TokenThumbnails.BySpecies(t.ID.Species), p.s);
            p.NudgeX();
            GUI.Label(p.Box(0.8f), t.ID.Name);
        }
        public static void InspectTokenButton(Token t, Panel p)
        {
            if (GUI.Button(p.FullBox, ""))
            {
                if (GUIInspector.LeftClick)
                {
                    GUIInspector.Inspected = t;
                    AVEffect.Highlight.Play(t);
                    CameraPanner.MoveTo(t);
                }
                //				if (GUIInspector.RightClick) {GUIInspector.ToolTip("Name");}
            }
            GUI.Box(p.Box(p.LineH), TokenThumbnails.BySpecies(t.ID.Species), p.s);
            p.NudgeX();
            p.NudgeY();
            FancyText.Highlight(p.Box(150), t.ID.FullName, p.s, t.Owner.Colors);
        }
        public static void InspectOnDeathButton(Token t, Panel p)
        {
            if (GUI.Button(p.FullBox, ""))
            {
                if (GUIInspector.LeftClick) 
                    GUIInspector.Inspected = TokenRegistry.Templates[t.OnDeath];
                if (GUIInspector.RightClick) 
                    TipInspector.Inspect(ETip.ONDEATH);
            }
            GUI.Box(p.Box(p.LineH), Icons.ONDEATH(), p.s);
            p.NudgeX();
            if (t.OnDeath == Species.None)
            {
                GUI.Label(p.Box(250), "(Leaves no remains)");
            }
            else
            {
                InspectTemplateButton(TokenRegistry.Templates[t.OnDeath], new Panel(p.Box(250), p.LineH, p.s));
            }


        }

    }
}
