   M S B A M L   `   `   `  ����K  GRestoranProjekat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:RestoranProjekat.FormeRestoranProjekat.Forme  5   	   "    RestoranProjekat.Forme.Racun   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:RestoranProjekat.Forme     5   	   -   5          �Name$  RacunProzor��5   	    � Title$ Racun��5   
   $	��517��6   $	��650��6%    � WindowStartupLocation$ CenterScreen=�61   $��Arial)�6V   $
��Bold"�6i   ��5	      
� 5
   
   .��5      �  � Color$ 	#FFDA6522��6    � Offset$ 0.735Q�6-   � 5      $ 	#FF5F405A��6   $ 0.925Q�6-   5      .��5      � .+�5   
   �� -   5         ��Name$ artikliLista��5      $
��Left=�6(   $	��359��6C   $��	28,94,0,0q�6P   $	��Top=�6c   $	��297��6{   
����������	#FF0032C8$��2,2,2,2q�6�   ��5      
� 5      .��5      � $ 	#FF3F80C8��6#   $ 0.922Q�65   � 5      $ 	#FF2F1FEF��6#   $ 1Q�65   5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   -   5         ��Name$ 	ArtikliLB��5      $��ARTIKLI��6#   $
��Left=�65   $��30��6P   $��
142,59,0,0q�6\   $	��Top=�6p   $��86��6�   $��16(�6�   $
��Bold"�6�   ���	#FFFFDE00$��2,2,2,2q�6�   ���	#FF2EF31E 5        5      5      �� 5    
   -   5        $ RacunListBox��5       $
��Left=�6(   $	��227��6C   $��362,202,0,0q�6P   $	��Top=�6e   $	��218��6}   
����������	#FF0032C8$��2,2,2,2q�6�   ��5!      
� 5"      .��5#      � $ 	#FF3F80C8��6#   $ 0.922Q�65   � 5$      $ 	#FF2F1FEF��6#   $ 1Q�65   5%       5'        5(      5*      �� 5+   
   -   5        $ RacunLB��5+      $��RACUN��6!   $
��Left=�61   $��30��6L   $��435,167,0,0q�6X   $	��Top=�6m   $��72��6�   $��16(�6�   $
��Bold"�6�   ���	#FFFFDE00$��2,2,2,2q�6�   ���	#FF2EF31E 5,        5-      5/      �� 50   
   -   5         ��Name$ 
PretragaTB��50      $
��Left=�6&   $��22��6A   $��
355,94,0,0q�6M   	 �� TextWrapping$
	 Wrap=�6a   $	��Top=�6u   $	��232��6�   ���	#FF42FF00���	#FFE9F513$��2,2,2,2q�6�   
 �� SelectionBrush
 �	#FFF1FF00 �� SelectionTextBrush �	#FFABFF00.��6A   51        52      .��53      54      �� 55   
   -   5        $ 
PretragaLB��55      $��	Pretraga:��6$   $
��Left=�68   $��30��6S   $��
355,59,0,0q�6_   $	��Top=�6s   $��66��6�   ���	#FFFFDE00$��2,2,2,2q�6�   ���	#FF2EF31E 56        57      59      �� 5:   
   -   5         ��Name$ DodajURacunButton��5:      $��Dodaj Na Racun��6,   $
��Left=�6E   $��31��6`   $��476,121,0,0q�6l   $	��Top=�6�   $	��111��6�   ���	#FFFFDE00���	#FF2EF31E$��2,2,2,2q�6�    5;        5<      5>      �� 5?   
   -	   5        $ ObrisiSaRacunaButton��5?      $��Obrisi sa Racuna��6/   $
��Left=�6J   $��31��6e   $��355,121,0,0q�6q   $	��Top=�6�   $	��109��6�   ���	#FFFFDE00���	#FF2EF31E$��2,2,2,2q�6�    5@        5A      5C      �� 5D   
   -
   5        $ SacuvajRacunButton��5D      $��Dodaj Na Racun��6-   $
��Left=�6F   $��31��6a   $��362,434,0,0q�6m   $	��Top=�6�   $	��119��6�   ���	#FFFFDE00���	#FF2EF31E$��2,2,2,2q�6�    5E        5F      5H      �� 5I   
   -   5        $ 
cenaRacuna��5I      $
��Left=�6&   $��31��6A   $��486,434,0,0q�6M   $
	 Wrap=�6b   $��0��6v   $	��Top=�6   $��94��6�   ���	#FF42FF00���	#FFE9F513$��2,2,2,2q�6�   
 �	#FFF1FF00 �	#FFABFF00.��6%   5J        5K      .��5L      5M      5O      5P      