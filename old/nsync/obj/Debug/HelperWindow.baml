   M S B A M L   `   `   `  ����	  nsync    nsync.HelperWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      -   5        $��windowHelper��5        � Title$
  Help��6   $	��100��6&   $	��350��63   |�.   � AllowsTransparency .  � WindowStyle$
 None=�6k    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6�    � Topmost .  
������  � 	Resources 5         DarkBorderBrush �� 
StartPoint �� EndPoint	 � Color
 � Offset��%�   &	        �� 5   
   $	 0,0&�66   $	 0,1&�6G   ��5	      � � 5
      $	 #363636��6#   $	
 0.0Q�63   � 5      $	 #000000��6#   $	
 1.0Q�63   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� -   5         g�Name$ FormFade��5      .��5      W� -   5        $ FormFadeAnimation��5   '    �� 
TargetName$ windowHelper��5   -    �� TargetProperty$ (Window.Opacity)�5   -    W� From$	 0.0X�5   -   	 W� To$	 1.0X�68    g� Duration$ 0:0:1D�6A    g� AutoReverse .   g� RepeatBehavior$ 1x��5   A   5      5      5      :� 5   
   $ Window.Unloaded��6   .[�5       �� .��5!      �� -   5        $ FormFadeOut��5!      .��5"      W� -   5        $ FormFadeOutAnimation��5"   '   $ windowHelper��5#   -   $ (Window.Opacity)�5$   -   $	 1.0X�5%   -   $	 0.0X�68   $ 0:0:1D�6A    .  $ 1x��5&   A   5(      5)      5*      .��5/      � .+�50      �� -   5        
���   � �� CornerRadius$ 20��50       #��[  62   $��2,2,2,2q�6a    J� Opacity$	 0.8Q�6�   ._�51   
   � .+�52      �� -   5        $��ButtonClose��52       � ToolTip$ Close��6(     ButtonCloseTemplate#��[ 68   $��15��6h   $��15��6s   $	��Top=�6�   $��Right=�6�   $	��8,5q�6�   6�   �� 53       �� Orientation$ 
Horizontal=�6   $��Center=�66   $��Center=�6Q   .+�54      �� -   5        $��ContentText��54       
�������$��Verdana)�6F   $��11(�6[   $��Center=�6i   $��Center=�6�   $	��350��6�   $
��Wrap=�6�    �� TextAlignment$ Center=�6�    �� Padding$ 8q�6�   6�   55      56      57      58      59      