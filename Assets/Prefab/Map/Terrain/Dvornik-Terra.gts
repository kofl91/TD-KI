GeoControl Version2
name.s
       Terrain_1
beschreibung.s

flag.b
    generateart.b
    Size.l
   bild.l
   keepseed.b
   seed.l
�
+ seamless.b
    features.b
   strength.l
d   undoliste.b
    weit.l
�R  hoch.l
�� hochu.l
    weitu.l
    max.f [16]/ min.f[16]
�  Malart.b
    brushsize.l
   matrixstrength.l
2   matrixundocounter.l
    brushseed.l
�
+ brushseedfix.b
    brushedge.b
    flowmapsize.l
    isonummer.l
    Endofallgemein
^          jMj?��=�?�V<�?�V<�n?`��=�k?!��=�k?!��=��j?R�=�3k?$w�=�k?^�=	k?!�=�k?->�=�k?->�=                        Endofallgemeinblock
Filterdatenblock
 �  filtertyp.w
    selid.l
����generateart.b
    levelan.w [12]
�  levelpower.w [12]
�  filtervalues.l [10]
	  terracesettings.l [10]
3	  filtername.s
_	  preset.s
l	  invert.b
    selectionnr.w
����mask.b
    filterkurve.l
����nummer.w
    Endoffilter
filtertyp.w
    selid.l
����generateart.b
    levelan.w [12]
n	  levelpower.w [12]
�	  filtervalues.l [10]
�	  terracesettings.l [10]
�	  filtername.s
�	  preset.s

  invert.b
    selectionnr.w
����mask.b
    filterkurve.l
����nummer.w
    Endoffilter
filtertyp.w
   selid.l
����generateart.b
    levelan.w [12]

  levelpower.w [12]
'
  filtervalues.l [10]
A
  terracesettings.l [10]
m
  filtername.s
�
  preset.s
�
  invert.b
    selectionnr.w
����mask.b
    filterkurve.l
    nummer.w
    Endoffilter
filtertyp.w
   selid.l
����generateart.b
    levelan.w [12]
�
  levelpower.w [12]
�
  filtervalues.l [10]
�
  terracesettings.l [10]
  filtername.s
1  preset.s
;  invert.b
    selectionnr.w
����mask.b
    filterkurve.l
    nummer.w
    Endoffilter
filtertyp.w
   selid.l
����generateart.b
    levelan.w [12]
=  levelpower.w [12]
W  filtervalues.l [10]
q  terracesettings.l [10]
�  filtername.s
�  preset.s
�  invert.b
    selectionnr.w
����mask.b
    filterkurve.l
    nummer.w
    Endoffilter
filtertyp.w
!   selid.l
   generateart.b
    levelan.w [12]
�  levelpower.w [12]
�  filtervalues.l [10]
  terracesettings.l [10]
<  filtername.s
h  preset.s
  invert.b
    selectionnr.w
����mask.b
    filterkurve.l
����nummer.w
    Endoffilter
    @ �            d d d d d d d d B B B     	            2   ��������        ����            �  �	  �  2   K   2               Basic Noise

    @ �            d 6 + $      	      	            2       ����        ����            �  �	  �  2   K   2               Selective Noise

    @ �            d d d d d d d d K d              2   Z   5   $                               �  `	  �  2   K   2               AddNoise

    @ �               d d d d d       K 2                  
   �   $                               �  `	  �  2   K   2               AddNoise

    @ �             
 ( d d d d d d K                        2                                   �  `	  �  2   K   2               Mountain Ridged

     @                d d d d d d d d d d d            F      2      ����       ����            �  �	  �  2   K   2               Soft fluvial wide (0)

Endoffilterdatenblock
Selections
 �  name.s
Selection  (0)
importpfad.s

typ.b
   id.l
    id2.l
����lfdnr.l
    ftyp.l
    slopean.b
    slopemin.w
    slopeminfuzzi.w
    slopemax.w
Z   slopemaxfuzzi.w
    slopemodus.b
   heightan.b
    heightmin.l
    heightminfuzzi.w
    heightmax.l
�  heightmaxfuzzi.w
    heightmodus.b
   orientationan.b
    orientation.w
    orientationangle.w
   orientfuzzimin.w
    orientfuzzimax.w
    orientmodus.b
   roughan.b
    roughmin.w
    roughminfuzzi.w
    roughmax.w
Z   roughmaxfuzzi.w
    roughmodus.b
   relativean.b
    relativemaxselect.b
   relativeminselect.b
    relativemodus.b
   Endofselection
name.s
Soft fluvial wide (0)
importpfad.s

typ.b
   id.l
   id2.l
   lfdnr.l
   ftyp.l
!   slopean.b
    slopemin.w
    slopeminfuzzi.w
    slopemax.w
Z   slopemaxfuzzi.w
    slopemodus.b
   heightan.b
    heightmin.l
    heightminfuzzi.w
    heightmax.l
�  heightmaxfuzzi.w
    heightmodus.b
   orientationan.b
    orientation.w
    orientationangle.w
   orientfuzzimin.w
    orientfuzzimax.w
    orientmodus.b
   roughan.b
    roughmin.w
    roughminfuzzi.w
    roughmax.w
Z   roughmaxfuzzi.w
    roughmodus.b
   relativean.b
    relativemaxselect.b
   relativeminselect.b
    relativemodus.b
   Endofselection
isodaten
�  iso()\name.s
�  iso()\isoid.l
    iso()\typ.b
    iso()\anzahl.l
    iso()\power.l
�  iso()\Size.l
   isodaten anzahl*3.w
�  Endofiso
endofisoliste
iso 0
� � � � Z 2 
             Shader
   �  colourtyp.b
����colourblending.w
����cblack.w
    cgrey.w
    cwhite.w
    cblendnoise.w
    distribution.w
����alphapower.b
d   alphablack.w
    alphagrey.w
   alphawhite.w
�   noiseblend.w
    disfilterkurve.w
    blendfilterkurve.w
    nummer.l
    ground.w[3]
�  colour1.w[3]
�  colour2.w[3]
�  gradient.s
�  name.s
  endofshader
colourtyp.b
   colourblending.w
����cblack.w
    cgrey.w
   cwhite.w
�   cblendnoise.w
    distribution.w
   alphapower.b
d   alphablack.w
    alphagrey.w
   alphawhite.w
�   noiseblend.w
    disfilterkurve.w
����blendfilterkurve.w
����nummer.l
   ground.w[3]
!  colour1.w[3]
'  colour2.w[3]
-  gradient.s
3  name.s
y  endofshader
endofshaderliste
  �   P P P � � � C:\Dokumente und Einstellungen\Johannes\Eigene Dateien\GeoControl_Entwicklung\Colours\full_landscape.bmp
Basic Shader
      �     � � � C:\Users\Kostiantyn\Documents\GeoControl2\Colours\full_landscape.bmp
Shader (1)
Renderset
z  camerahoehe.l
   zoom.l
4   entfernung.l
�  ziel.l
D  hshift.l
   Perspektive.l
�   sunheight.l
   sunposition.l
  sunpower.l
   ambient.l
@   ambientcontrast.l
    ambientblue.l
   fog.l
   endofrenderset
Springs
�  pwFilterdatenblock
Endoffilterdatenblock
Filterkurven
�  punkte
�  punkte
�  endofblock
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       	 
 
                                 ! " # $ & & ' ( ( ) * + , - . . / 0 1 2 2 3 4 4 6 7 8 9 ; < = > ? A B D G H J L N P R T V X Y \ ^ a d f h k m o q u w z |  � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � � �   !#%&(,.0248:<?CDFJLPQRUXZ[]_`abdfgijkmnopqsuwyz|}���������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������                                                                                     pwfilternummern64
                                                                                                                                                                                                                                                                    Vectorkurven
/%  punkte
-!  endofblock
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  Vector
;%  