   M S B A M L   `   `   `  ����@  <nsync, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:nsync.Propertiesnsync.Properties  5          nsync.VisualPreviewWindow   5   	   W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      .constclr-namespace:nsync.Properties    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      -   5        $��WindowVisualPreview��5   �     � Title$  nsync��6   $	��598��6   $	��725��6     � 
ResizeMode$ NoResize=�6,    � AllowsTransparency .  � WindowStyle$
 None=�6\    � WindowStartupLocation$ CenterOwner=�6o    � ShowInTaskbar .   � DataContext 5      ��  �� RelativeSource �� *Self+��5      �� 5	   
    �� ImageSource$% Resources/visualpreviewback.png��6   .��5      � .+�5   
   �� -   5        	 ��Name$	 TitleBar��5   G   $��38��6Y   $	��Top=�6e   �� 5   
   -   5        $��ButtonClose��5          nsync.Properties.Resources
   closeToolTip � ToolTip# Z
 6$      ButtonCloseTemplate#��[  6V   $��25��6�   $��23��6�   $	��Top=�6�   $��Right=�6�   $��15,10q�6�   6�   � 5   
   $��Center=�6   $	��Top=�6,   $
��0,75q�6D   .+�5      �� -   5        $��BoxLeftPath��5      $	��328��6,   $��41��68   $
��Left=�6D   $	��Top=�6_   $��0,0,0,0q�6w    J� 	AllowDrop .    BoxVisualPreviewPathStyle#��[ 6�   .+�5      �� -   5        $��LabelLeftPath��5   d   
�������$
��Left=�6,   $��10,10q�6G   $��10(�6V   $	��Top=�6y   .��6�   
LeftPath6�   5      �� 5      -   5        $��BoxRightPath��5      $	��328��6-   $��41��69   $
��Left=�6E   $	��Top=�6`   $��	340,0,0,0q�6x    .  #��[ 6�   .+�5      �� -   5        $��LabelRightPath��5   c   
�������$
��Left=�6+   $��10,10q�6F   $��10(�6U   $	��Top=�6y   .��6�   	RightPath6�   5      5      �� 5   
   -   5         ��Name$ ComboBoxFilter��5      $	��Top=�6+   $	��148��6C   $��32��6O     ComboBoxFileTypeTemplate#���  6[   
�������$��Right=�6�   $��
0,123,30,0q�6�    � Cursor$
 Hand��6�   .@�5      �� -	   5        $��
FilterBoth��5      .��6-   Both Folders6;   �� 5      -
   5        $��
FilterLeft��5      .��6-   Left Folder6:   �� 5      -   5        $��FilterRight��5      .��6.   Right Folder6<   5      �� 5   
   -   5        $��LabelChangeFilter��5   G   
�������$��25.96��6#   $��0,123,185,0q�62   $	��Top=�6`   $��Right=�6x   .��6�   Show changes in:6�   �� 5   
   -   5        $��BoxVisualPreview��5      $	��672��6-   $	��418��69   $��Center=�6F   $��Bottom=�6c   $��0,0,0,25q�6~    .    BoxVisualPreviewStyle#��[ 6�    � 	Resources 5        leftRightFileNameTemplate  bothFileNameTemplate��%�  &	       &	 �     �� 5      .R�5      �� $
��-6,0q�6j   ��6    �� *leftRightFileName+��6C   �� *leftRightColor+ 5       _� 5!   "   .��5"   &   �� ��60   �� *leftRightToolTip+5#   #   5%      5&      �� 5'      .R�5(      �� $
��-6,0q�6`   ��6    �� *bothFileName+��6>   �� *	bothColor+ 5)      _� 5*   "   .��5+   &   �� ��60   �� *bothToolTip+5,   #   5.      5/      50      .+�51      �� -   5        $��ListViewBoth��51      $
��8, 5q�6   
�������$	��410��6B   $��Disabled=�6R   $	��670��6�   $
 Hand��6�   ��6�   �� *BothPreviewCollection+ �� View 52      �� 53      .P�54   
   ��  �� Width$ 86��6    �� DisplayMemberBinding 6$   �� *
bothLeft+.��55      �� -   5        
 � Tag$ bothLeft��55   5   $
��Left��6D   56      �� 57   
   $	 474��6    �� CellTemplate# [ 6%   .��58      �� -   5        $ bothFileNameTemplate��58   5   $��Filename��6P   59      �� 5:   
   $ 86��6    6$   �� *	bothRight+.��5;      �� -   5        $ 	bothRight��5;   5   $��Right��6E   5<      5=   	   5?      �� 5@      -   5        $��ListViewLeftRight��5@   %   $
��8, 5q�6   
�������$	��410��6Q   $��Disabled=�6a   $	��670��6�   $
��Left=�6�   $y�	Collapsed=�6�   $
 Hand��6  ��6�   �� *LeftRightPreviewCollection+ 5A      �� 5B      .P�5C      �� $	 560��6)   # [ 65   .��5D      �� -   5        $ leftRightFileName��5D   E   $��Filename��6]   5E      �� 5F      $ 86��6)    64   �� *leftRightAction+.��5G      �� -   5        $ leftRightAction��5G   E   $��Action��6[   5H      5I      5K      5L      �� 5M   
   -   5        $��LabelNoChanges��5M   T   
�������$��Center=�6$   $��	0,185,0,0q�6A   $	��Top=�6j   $��25.96��6�   $��74.847��6�   $y�Hidden=�6�   .��6�   
No Changes6�   5N      5O      