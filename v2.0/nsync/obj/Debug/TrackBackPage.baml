   M S B A M L   `   `   `  ����@  <nsync, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:nsyncnsync  5      3clr-namespace:nsync.Propertiesnsync.Properties  5          nsync.TrackBackPage   5      W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      %nsyncnsclr-namespace:nsync    5      .constclr-namespace:nsync.Properties    5      -   5          N� Title$  TrackBackPage��5      $	��518��6   $	��287��6'    � DataContext 5      ��  �� RelativeSource �� *Self+.��5      � .+�5	      � -   5        $��GridTrackBack��5	      .+�5
   
   �� �� 5   
   -   5         ��Name$ ImageTrackBack��5      $��5q�6(   $	��Top=�63   $��45��6K   $��Resources/trackback.png��6W   $	��210��6x   �� 5   
   -   5         ��Name$ ComboBoxSourceFolder��5      $��Verdana)�61   $��11(�6F   $��
20,55,20,0q�6�   $	��Top=�6�   $��29��6�      ComboBoxSourceFolderStyle#���   6�   
������� � Cursor$
 Hand��6  �� 5   
   -   5        $��BoxTrackBack��5      $��15,100,15,42q�6)    J� 	AllowDrop .    BoxVisualPreviewStyle#��[ 6Q    �� Orientation$ Vertical=�6�   $	��488��6�   $	��145��6�   .+�5      �� -   5        $��ListViewForLeftFolder��5      $
��5, 3q�64   $	��140��6B   
�������$	��140��6e   $	��478��6u   $
 Hand��6�   $y�Visible=�6�     TrackBackItemStyle#��[ 6�   ��6�   �� *" TrackBackCollectionForLeftFolder+ �� View 5      �� 5      .P�5      �� 	 �� Width$
	 Auto��6)   
 �� DisplayMemberBinding
 66   �� *
nameItem+��5      �� 5   "   -   5        $��Folder Name��5   7   
 � Tag$ nameItem��6M   $
��Left=�6n   5      �� 5      $
	 Auto��6)   
 66   �� *
dateItem+��5      �� 5   "   -   5        $��Date & Time��5   7   $ dateItem��6Q   $
��Left=�6r   5      �� 5      
 6)   �� *
folderItem+��5      �� 5   "   -	   5        $��Folder synced with��5   7   $ 
folderItem��6T   $
��Left=�6w   5      5       5"      �� 5#      -
   5        $��ListViewForRightFolder��5#      $
��5, 3q�65   $	��140��6C   
�������$	��140��6f   $	��478��6v   $
 Hand��6�   $y�	Collapsed=�6�   #��[ 6�   ��6�   �� *#!TrackBackCollectionForRightFolder+ 5$      �� 5%      .P�5&      �� $
	 Auto��6)   
 66   �� *
nameItem+��5'      �� 5(   "   -   5        $��Folder Name��5(   7   $ nameItem��6M   $
��Left=�6n   5*      �� 5+      $
	 Auto��6)   
 66   �� *
dateItem+��5,      �� 5-   "   -   5        $��Date & Time��5-   7   $ dateItem��6Q   $
��Left=�6r   5/      �� 50      
 6)   �� *
folderItem+��51      �� 52   "   -   5        $��Folder synced with��52   7   $ 
folderItem��6T   $
��Left=�6w   54      55      57      58      �� 59   
   -   5        $��LabelNoChanges��59   L   $��28��6   $��Center=�6   $��	0,150,0,0q�69   $	��Top=�6b   
�������$y�Hidden=�6�   .��6�   No TrackBack data6�   �� 5:   
   -   5        $��LabelProgress��5:   J   $��28��6   $
��Left=�6   $��	20,0,0,10q�67   $��Bottom=�6_   $	��140��6z   
�������$y�Hidden=�6�   6�   �� 5;   
   -   5        $��ButtonRestore��5;   [   $��Bottom=�6   $��Right=�6,   $��	0,0,15,10q�6H       nsync.Properties.Resources   restoreToolTip � ToolTip# Z 6p     ButtonRestoreTemplate#��[ 6�   $��23��6�   $��30��6�   6     nsync.GIFImageControl  5<   
   -   5          Name$ RestoringImage��5<   "      	GIFSource$ load-reverse.gif��6:      AllowClickToPause .  $��23��6r   $��23��6}   $��Bottom=�6�   $��Right=�6�   $��	0,0,18,15q�6�   $y�Hidden=�6�   5=      �� 5>   
   -   5        $��LabelDisabled��5>   L   $��28��6   $��Center=�6   $��	20,0,0,10q�69   $��Bottom=�6a   
�������$y�Hidden=�6�   .��6�   64TrackBack is disabled. Enable it in settings to use.6�   5?      5@      