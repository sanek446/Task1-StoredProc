   M S B A M L   `   `   `  ����F  BTask1WithUC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Task1WithUCTask1WithUC  5          Task1WithUC.UC   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Task1WithUC     5      .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5           Name$  dataGrid��5
      $
��Left=�6%   $��	22,36,0,0q�6@   $	��Top=�6S   $	��350��6k   $	��510��6x      EnableRowVirtualization .  6�   �� 5   
   -   5         ��Name$ comboBox��5      $
��Left=�6%   $��	105,6,0,0q�6@   $	��Top=�6S   $��44��6k   $��20��6v   6�   �� 5   
   -   5        $ 	comboBox1��5      $
��Left=�6&   $��23,6,0,0q�6A   $	��Top=�6S   $��83��6k   $��20��6�   ��6�   ��    System.Windows.Media.Fonts   SystemFontFamilies �� Source# Z 6�   �� 5   
   -   5         ��Name$ buttonChoose��5      $��Excel��6'   $
��Left=�67   $��	299,6,0,0q�6R   $	��Top=�6e   $��45��6}   $��20��6�   5      �� 5   
   -   5         ��Name$ textBox��5      $
��Left=�6#   $��20��6>   $��	160,6,0,0q�6J    �� TextWrapping$
 Wrap=�6]   $	��Top=�6q   $��76��6�   6�   �� 5   
   -   5        $ 
buttonFind��5      $
��Find��6%   $
��Left=�64   $��	236,6,0,0q�6O   $	��Top=�6b   $��50��6z   6�   �� 5   
   -   5        $ buttonDelete��5      $��Delete��6'   $
��Left=�68   $��	356,6,0,0q�6S   $	��Top=�6f   $��50��6~   6�   � 5   
   -   5        $��
popCountry��5      $��Mouse=�6"    � 	StaysOpen .  $	��200��6�   	 � PlacementTarget	 64   �� 
 �� ElementName$
 Button_Filter_Click��.��5      �� 
�������
�������$��1,1,1,1q�6;   $	��200��6U   $��-2,0,2,0q�6b   ._�5      �� -	   5         ��Name$ lstCountries��5      $��0q�60   ��5      �� 5      .R�5      ��  d� 	IsChecked 6'   �� *	IsChecked+��6G   �� *Item+5      5      5      5      5       5!      