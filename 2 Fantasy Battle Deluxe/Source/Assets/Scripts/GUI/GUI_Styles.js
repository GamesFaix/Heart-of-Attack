#pragma strict

var fonts: Font[] = new Font[10];
var sButton: GUIStyle[] = new GUIStyle[10];
var label: GUIStyle = new GUIStyle();
var heading: GUIStyle[] = new GUIStyle[2];

function Awake(){
	fonts[0]=Resources.Load("Fonts/Arial") as Font;
	fonts[1]=Resources.Load("Fonts/TeamFonts/GEARP") as Font;
	fonts[2]=Resources.Load("Fonts/TeamFonts/NewRepublic") as Font;
	fonts[3]=Resources.Load("Fonts/TeamFonts/Torridale") as Font;
	fonts[4]=Resources.Load("Fonts/TeamFonts/ForgottenGrove") as Font;
	fonts[5]=Resources.Load("Fonts/TeamFonts/Chrononista") as Font;
	fonts[6]=Resources.Load("Fonts/TeamFonts/PsychoTropic") as Font;
	fonts[7]=Resources.Load("Fonts/TeamFonts/PsilentAureator") as Font;
	fonts[8]=Resources.Load("Fonts/TeamFonts/Voidoid") as Font;
	fonts[9]=Resources.Load("Fonts/Arial") as Font;
	
	var i: byte;
	for (i=0; i<sButton.length; i++){
		sButton[i].font=fonts[i];
		sButton[i].normal.textColor=Color.white;
		sButton[i].hover.textColor=Color.white;
		sButton[i].active.textColor=Color.white;
		sButton[i].normal.background=Resources.Load("gui/style/button") as Texture2D;
		sButton[i].hover.background=Resources.Load("gui/style/button_hover") as Texture2D;
		sButton[i].active.background=Resources.Load("gui/style/button_active") as Texture2D;
		sButton[i].alignment=TextAnchor.MiddleCenter;
	}

	label.font=fonts[0];
	label.normal.textColor=Color.white;
	label.normal.background=Resources.Load("gui/style/box") as Texture2D;
	label.alignment=TextAnchor.MiddleCenter;

	heading[0].font=Resources.Load("Fonts/Heading1") as Font;
	heading[0].normal.textColor=Color.white;
	heading[0].normal.background=Resources.Load("gui/style/box") as Texture2D;
	heading[0].alignment=TextAnchor.MiddleCenter;
	
	heading[1].font=Resources.Load("Fonts/Heading2") as Font;
	heading[1].normal.textColor=Color.white;
	//heading[1].normal.background=Resources.Load("gui/style/box") as Texture2D;
	heading[1].alignment=TextAnchor.MiddleCenter;
	
}