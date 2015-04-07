using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class UltimateEscape : PhysicsGame
{
    double nopeusYlos=500;
    Vector kiipeaYlos = new Vector(0, 200); 
    double nopeusVasemmalle=350;
    double nopeusOikealle = -350;
    PlatformCharacter pelaaja;
    Image taustakuva = LoadImage("taistis");
    Image Vihollinen = LoadImage("vihollinen");
    Image tikkuukko = LoadImage("oik");  
    private Animation Kiipeaminen;
    List<int> Lista;
    PlatformCharacter vihollinen;
    AssaultRifle vihase;
    PhysicsObject[] viholliset;
    int esjonne;
    public override void Begin()
    {
         Lista = new List<int>();
        Level.Background.Image = taustakuva;
      
        Level.Background.Width = Screen.Width;
        Level.Background.Height = Screen.Height;
        luovalikko();
        viholliset = new PhysicsObject[5];
     
    }
    void LuoKentta(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject mappi = PhysicsObject.CreateStaticObject(leveys, korkeus);
        mappi.Position = paikka;
        

        Add(mappi);
        mappi.Color=Color.Black;
       
    }
    void luopelaaja(Vector paikka, double leveys, double korkeus)
    {
       
        
        pelaaja = new PlatformCharacter(100, 100);
        pelaaja.Image = tikkuukko;
        pelaaja.Position = paikka;
        Add(pelaaja);
        AddCollisionHandler(pelaaja, pelaajatormasi);
      pelaaja.Weapon = new AssaultRifle(50, 30);
     pelaaja.Weapon.Ammo.Value = 100;
     pelaaja.Weapon.ProjectileCollision = AmmusOsui;
     pelaaja.Weapon.Power.DefaultValue = 500;
     pelaaja.Weapon.X += 25;
     pelaaja.Weapon.Y += 20;
    }
    void luovalikko()
    {
        MultiSelectWindow alkuValikko = new MultiSelectWindow("Ultimate Escape",
    "Aloita peli",  "Lopeta");
        Add(alkuValikko);
        alkuValikko.AddItemHandler(0, aloitapeli);
        
        alkuValikko.AddItemHandler(1, Exit);
       
    }
    void luocontrors()
    {
        Keyboard.Listen(Key.A, ButtonState.Down, LiikutaHahmoa, "liiku vasemmalle",nopeusOikealle);
       Keyboard.Listen(Key.D, ButtonState.Down, LiikutaHahmoa, "liiku oikealle", nopeusVasemmalle);
       Keyboard.Listen(Key.W, ButtonState.Down, PelaajaHyppaa, "hyppaa", nopeusYlos);
       Keyboard.Listen(Key.Space, ButtonState.Down, PelaajaAmpuu, "Ammu");
       Keyboard.Listen(Key.E, ButtonState.Down, kiipea, "kiipea",1,kiipeaYlos);
       Keyboard.Listen(Key.E, ButtonState.Released,kiipea,"stopanim",2,Vector.Zero);
}

    void aloitapeli()
    {
        Level.Background.Image=null;
 ColorTileMap ruudut = ColorTileMap.FromLevelAsset("mappi");
        Camera.Zoom(0.6);
        ruudut.SetTileMethod(Color.Black, LuoKentta);
        ruudut.SetTileMethod(Color.FromHexCode("00FF21"), luopelaaja);
        ruudut.SetTileMethod(Color.FromHexCode("FF0000"), luoulospaasy);
        ruudut.SetTileMethod(Color.FromHexCode("FFD800"), luovihollinen);

        ruudut.Execute(20, 20);
        Kiipeaminen = LoadAnimation("anima");
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
       
        Gravity = new Vector(0.0, -800.0);
        Level.Background.Color = Color.White;
        LuoPistelaskuri();
        luocontrors();
        
        
    }
    void LuoPistelaskuri()
    {
       IntMeter pisteLaskuri = new IntMeter(30);
      
        Label pisteNaytto = new Label();
        pisteNaytto.X = Screen.Left + 100;
        pisteNaytto.Y = Screen.Top - 100;
        pisteNaytto.TextColor = Color.Black;
        pisteNaytto.Color = Color.White;
        pisteNaytto.Title = "Lives left";
        pisteNaytto.BindTo(pisteLaskuri);
        Add(pisteNaytto);
    }
    void LiikutaHahmoa(double nopeus)
    {
        pelaaja.Walk(nopeus);
        



    }
    void PelaajaHyppaa(double nopeus)
    {
        pelaaja.Jump(nopeus);
        

    }
    void kiipea(int animstop,Vector nopeus)
    {
        pelaaja.Animation = new Animation(Kiipeaminen);
        pelaaja.Animation= (Kiipeaminen);
        pelaaja.Velocity = nopeus;
        
        if (animstop == 1)
        {

         pelaaja.Animation.Start();
        }
        else if (animstop == 2)
        {
            pelaaja.Image = tikkuukko;  
pelaaja.Animation.Stop();
        }
    }
    void luoulospaasy(Vector paikka, double leveys, double korkeus)
    {
        int luku = RandomGen.NextInt(4);
        PhysicsObject sulku = PhysicsObject.CreateStaticObject(50, 50);
        Add(sulku);
        sulku.Position=paikka;
        sulku.Color = Color.Black;
        sulku.Tag = "sulku"+luku;
        Lista.Add(4);
       

    }
    void pelaajatormasi(PhysicsObject tormaaja, PhysicsObject kohde)
    {
        if (kohde.Tag.ToString() == "sulku1")
        {
            ClearAll();
            aloitapeli();
            
        
        }


    
    
    
    }
    void PelaajaAmpuu()
    {
        PhysicsObject ammus = pelaaja.Weapon.Shoot();


    }
    void AmmusOsui(PhysicsObject ammus, PhysicsObject kohde)
    {
        ammus.Destroy();
        if (kohde.Tag.ToString() == "destroy")
        {
            kohde.Destroy();
        }


    }
    void luovihollinen(Vector paikka, double leveys, double korkeus)
    {
         vihollinen = new PlatformCharacter(100, 100);
        vihollinen.Image = Vihollinen;
        vihollinen.Position = paikka;
        Add(vihollinen);
        AddCollisionHandler(vihollinen, pelaajatormasi);
        vihollinen.Tag = "destroy";
       
        vihase = new AssaultRifle(50, 30);
        
        vihase.ProjectileCollision = AmmusOsui;
        vihase.Power.DefaultValue = 500;
        vihase.X += 25;
        vihase.Y += 20;
        vihollinen.Weapon=vihase;
        luoaivot();
        Timer ajastin = new Timer();
        ajastin.Interval = 2.0;
        ajastin.Timeout += delegate { vihollinenampuu(vihollinen); };
        ajastin.Start();
        viholliset[esjonne] = new PhysicsObject(40, 40);
    
    }
  
    void luoaivot()
    {
        PlatformWandererBrain seuraajanAivot = new PlatformWandererBrain();
        seuraajanAivot.Active = true;
        seuraajanAivot.Speed = 300;                
       // seuraajanAivot.DistanceFar = 600;           
        //seuraajanAivot.DistanceClose = 200;         
        //seuraajanAivot.StopWhenTargetClose = true;
        
        vihollinen.Brain = seuraajanAivot;
      


    }
    void vihollinenampuu(PlatformCharacter ase)
    {
        ase.Weapon.Shoot();
         

    }
}













