   M S B A M L   `   `   `  ����F  BTask1WithUC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Task1WithUCTask1WithUC  5   	       Task1WithUC.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Task1WithUC     5   	     � Title$  
MainWindow��5   	   $	��500��6   $	��900��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ textBox��5
      $
��Left=�6#   $��75��6>   $��
122,14,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��195��6�   �� 5   
   -   5         ��Name$ label��5      $��Connection string:��6   $
��Left=�6<   $��8,10,0,0q�6W   $	��Top=�6i      Task1WithUC.UC  5   
   -   5          Name$
 MyUC��5      $
��Left=�6!   $��
346,14,0,0q�6<   $	��Top=�6P   �� 5   
   -   5         ��Name$ button1��5      $��Run stored procedure��6"   $
��Left=�6A   $��122,156,0,0q�6\   $	��Top=�6q   $	��131��6�   6�   �� 5   
   -   5        $ textBox2��5      $
��Left=�6$   $��23��6?   $��130,418,0,0q�6K   $
 Wrap=�6`   $��0��6t   $	��Top=�6}   $��45��6�   �� 5   
   -   5        $ button2��5      $��Block column No��6"   $
��Left=�6<   $��
14,418,0,0q�6W   $	��Top=�6k   $	��107��6�   6�   �� 5   
   -   5        $ textBox1��5      $
��Left=�6$   $��23��6?   $��123,110,0,0q�6K   $
 Wrap=�6`   $��selectSP��6t   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ label1��5      $��Stored procedure:��6    $
��Left=�6<   $��
12,106,0,0q�6W   $	��Top=�6k   �� 5   
   -	   5        $ label2��5      $��SP for delete��6    $
��Left=�68   $��
24,217,0,0q�6S   $	��Top=�6g   �� 5   
   -
   5        $ textBox3��5      $
��Left=�6$   $��23��6?   $��110,219,0,0q�6K   $
 Wrap=�6`   $��deleteSP��6t   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ label3��5      $��SP for insert��6    $
��Left=�68   $��
24,252,0,0q�6S   $	��Top=�6g   �� 5   
   -   5        $ label4��5      $��SP for update��6    $
��Left=�68   $��
24,286,0,0q�6S   $	��Top=�6g   �� 5   
   -   5        $ textBox4��5      $
��Left=�6$   $��23��6?   $��110,258,0,0q�6K   $
 Wrap=�6`   $��insertSP��6t   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ textBox5��5      $
��Left=�6$   $��23��6?   $��110,291,0,0q�6K   $
 Wrap=�6`   $��updateSP��6t   $	��Top=�6�   $	��120��6�   5      5      