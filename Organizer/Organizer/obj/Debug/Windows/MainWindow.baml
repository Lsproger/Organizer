   M S B A M L   `   `   `  ����D  @Organizer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Organizer	Organizer  5   	   %clr-namespace:Organizer	Organizer  5
   	       Organizer.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Organizer     5   	   MCustom@http://schemas.microsoft.com/winfx/2006/xaml/presentation/ribbon  5   	   )databindingclr-namespace:Organizer  5
   	   -   5          �Name$  MainWindow1��5   	    � Title$ 	Organizer��5   	   $	��597��6   $	��934��6(   $	��934��64   $	��597��6C   $	��597��6S   $	��934��6c   
���   �
���   � � Icon$- '/Organizer;component/Resources/icon.ico��6�    � 
ResizeMode$ CanMinimize=�6�   ��6r   Z�  � 	Resources 5         Organizer.DayConverter   DayConverter   Organizer.LessonConverter  LessonConverter&    Organizer.ProgressStateConverter  ProgressStateConverter  _addNoteImage �� ImageSource  _minusImage  
_plusImage  _sendMessageImage��%e  &	        &	      &	 &     &	 9     &	 u     &	 �     &	 �       5   
   6:     5   
   6@     5   
   6N   �� 5   
   $( "@..\..\..\Resources\addNotePic.png��6+   �� 5   
   $# @..\..\..\Resources\minus.png��6)   �� 5   
   $" @..\..\..\Resources\plus.png��6(   �� 5   
   $) #@..\..\..\Resources\sendMessage.png��6/   5      .��5      � $��0q�6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      � 	 � Color$	 	#FF5DEEE7��6   � 5      $	 	#FF00A2FF��6   
 � Offset$
 1Q�61   5      .+�5    
   �� -   5        $��MainTabControl��5       $��20,10,30,10q�6+   ���	#FFDAFAFF$��0q�6n   ��6W   Z� .@�5!      �� -   5        $��ProgressTabItem��5!      $��ПРОГРЕСС��6-   $��30��6?   $
��Auto��6K   $��-3,0,0,0q�6X   
����ح�$��Stretch=�6�   $��1,0,0,0q�6�   ��6�   Z� .��5"      ]� ��. .+�5#      �� $
��0,20q�6!   $	��Top=�6/    �� Orientation$ 
Horizontal=�6D   $��50��6]   .+�5$      �� -   5        $��_lessonsBox��5$   =   $	��250��6#   $��24(�6/   6=   4  .Microsoft.Windows.Controls.Ribbon.RibbonButton  5%      -   5        $��50��5%   .   #��[ 6T      FocusedBackground# �  6�      MouseOverBackground# �  6�   �� 5&      -   5        $"��NotificationMessgeToProgress��5&   �   $��Papyrus)�6    $	��550��65   $��Right=�6A   $��Center=�6d   $��20(�6�   ���	#FFF32E2E6�   5'      �� 5(      -   5        $��_progressList��5(      
������   ListBoxNews#��[ 6L   ��5)      �� 5*      .R�5+   "   � $	��750��6'   .+�5,   &   ]� ��. $	��5,0q�6F   .+�5-   *   �� $��Right=�65   $ Vertical=�6L   .+�5.   .   �� $ 
Horizontal=�69   .+�5/   2   �� $	��100��68   $��Всего заданий��6D   �� 50   2   $��30��68   $��Center=�6C   ��6e   �� *NeededTasks+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 51   2   .+�52   6     -   5        $��15��52   J   $��15��6s      Label$ +��6   ���	#FFA4A5A66�     53   6   -	   5        $��15��53   J   $��15��6t   $ -��6�   ���	#FFA4A5A66�   54   3     55   2   -
   5        $��24��55   b   $��20(�6m   $ x��6{   56   /   �� 57   .   $ 
Horizontal=�69   .+�58   2   �� $	��100��68   $��Сдано заданий��6D   �� 59   2   $��30��68   $��Center=�6C   ��6e   �� *CompletedTasks+$ TwoWay=�$ PropertyChanged=��� 5:   2   $ Vertical=�6=   .+�5;   6     -   5        $��15��5;   J   $��15��6v   ���	#FFA4A5A6$ +��6�     5<   6   -   5        $��15��5<   J   $��15��6w   ���	#FFA4A5A6$ -��6�   5=   3   5>   /   5?   +   �� 5@   *   .+�5A   .   �� $��Center=�6S   ��64   �� *
LessonName+6S   � 5B   .    � Minimum$ 0Q�6:    � Maximum$	 100Q�6F   $��30��6�    � Value 6T   �� *TaskProgress+$ TwoWay=�$ PropertyChanged=� �� 	Converter# [ 6�   5C   +   5D   '   5E   #   5F      5H      5I      5J      �� 5K      $��Заметки��6   $��-3,0,0,0q�6'   $��1,0,0,0q�69   ��6S   Z� ��5L      �� 5M      $	 1,1&�6*   $	 0,1&�69   .��5N      � $	 	LightBlue��6'   $
 1Q�69   � 5O      $	 	#FF6C98CD��6'   � 5P      $	 	LightBlue��6'   $
 0.042Q�69   5Q      .��5S      �  � ColumnDefinitions 5T      �� 5U      $��* �6+   �� 5V      $��* �6+   .+�5X      ]� ��. $��1��65   .+�5Y      �� -   5        $��	_noteText��5Y   o   $	��300��6"   $	��400��6.    �� TextWrapping$
 Wrap=�6[    J� IsManipulationEnabled . $��Segoe Print)�6�   $��14(�6�   ���	#FFFBF6906�   5Z      � 5[      $��0��6   .+�5\      �� .+�5]      &   System.Windows.Controls.Calendar  -   5        $��	_calendar��5]   E      SelectionMode$ MultipleRange=�6'   6E   �� 5^      $ 
Horizontal=�6)   .+�5_   "     -   5        $��AddNoteButton��5_   o   $��20��66   $ Добавить��6B   $��
140,0,50,0q�6�    �	#7F3EF1216�     5`   "   -   5        $��DeleteNoteButton��5`   R   $��20��66   $ Удалить��6B    �	#7FF51C1C6�   5a      �� 5b      $
��Left=�6$   $
��20,0q�6F   $��16(�6T   $-��'Существующие заметки��6b   �� 5c      -   5        $��ExistingNotesList��5c   &   $
��20,0q�6?   $	��245��6M   ��5d   "   �� 5e   &   .R�5f   *   �� $ 
Horizontal=�65   $	��5,0q�6O   .+�5g   .   �� $	��100��68   ��6D   ��  �� Path$ NoteDate��� 5h   .   $	��200��68   ��6D   �� $ NoteDescription�5i   +   5j   '   5l      5m      5n      5o      5p      �� 5q      $��РАСПИСАНИЕ��6   $��-3,0,0,0q�6*   $��1,0,0,0q�6<   ��6V   Z� ��5r      �� 5s      $	 1,1&�6*   $	 0,1&�69   .��5t      � $	 	LightBlue��6'   $
 1Q�69   � 5u      $	 	#FF6C98CD��6'   � 5v      $	 	LightBlue��6'   $
 0.042Q�69   5w      .��5y      � .+�5z      ]� ��. .+�5{      �� $ 
Horizontal=�6%   $	��Top=�6>    � DataContext 6S   �� *SelectedTimeTable+.+�5|      ��  �� 
IsReadOnly . $��Center=�68   $	��100��6Z    � Cursor$ Arrow��6�   ��6f   �� *Day+# [  $ 	LostFocus=�$ TwoWay=�6�   �� 5}       . $��Center=�68   $��70��6Z   $ Arrow��6�   ��6e   �� *LessonNumber+# [ $ 	LostFocus=�$ TwoWay=�6�   �� 5~      $��10,0,0,0q�6(   $��Center=�6:   $��Аудитория��6U   �� 5      -   5        $��5,0,15,0q�5   &   $��Center=�68   $��70��6Z   ��6e   �� *
Auditorium+$ 	LostFocus=�$ TwoWay=�6�   �� 5�      $��Center=�6(   $��Тип��6C   �� 5�      -   5        $��5,0,15,0q�5�   &   $��Center=�68   $��50��6\   ��6g   �� *
LessonType+$ 	LostFocus=�$ TwoWay=�6�   �� 5�      $��Center=�6(   $��Предмет��6C   �� 5�      -   5        $��5,0,15,0q�5�   &   $��Center=�68   $	��200��6Z   ��6f   �� *
LessonName+$ 	LostFocus=�$ TwoWay=�6�   �� 5�      $��Center=�6(   $!��Учебная неделя��6C   �� 5�      -   5        $��_week��5�   4   $	��5,0q�6'   .@�5�   "   �� $��Первая��6/   �� 5�   "   .��6/   Вторая5�   #   5�      5�      �� 5�      -   5        $��_lessons��5�   �     ListBoxLessons#��[ 6"   $��0q�6W   $��Right=�6c   $	��500��6z   $��Disabled=�6�   ��.  $��Disabled=�61   �� SelectedItem 6�   �� *SelectedTimeTable+��6  Z� ��5�      �� 5�   "   .R�5�   &   �� $ 
Horizontal=�61   $��0q�6J   $��16��6U    �� CanVerticallyScroll .   J� OpacityMask 6}   Z� .+�5�   *   �� $��10,0,0,0q�65   $��70��6G   ��6R   �� $ LessonNumber�# [ $ PropertyChanged=�$ TwoWay=��� 5�   *   $��50��64   ��6?   �� $ 
Auditorium�$ PropertyChanged=�$ TwoWay=��� 5�   *   $��30��64   ��6?   �� $ 
LessonType�$ PropertyChanged=�$ TwoWay=��� 5�   *   $	��300��64   ��6@   �� $ 
LessonName�$ PropertyChanged=�$ TwoWay=�5�   '   5�   #   5�      �� 5�      $ Vertical=�6%   .+�5�      �� $��81��6$   $��Понедельник��60   $��30(�6F   $��Center=�6T   ���	#FFF7F5F56v   �� 5�      $��81��6$   $��Вторник��60   $��30(�6B   $��Center=�6P   ���	#FFF5E8DE6r   �� 5�      $��81��6$   $��
Среда��60   $��30(�6@   $��Center=�6N   ���	#FFF1F1836p   �� 5�      $��81��6$   $��Четверг��60   $��30(�6B   $��Center=�6P   ���	#FFF3F3516r   �� 5�      $��81��6$   $��Пятница��60   $��30(�6B   $��Center=�6P   ���	#FFFFFF2E6r   �� 5�      $��81��6$   $��Суббота��60   $��30(�6B   $��Center=�6P   ���	#FFC5F71B6r   5�      5�      5�      5�      �� 5�      $%��ДОСКА ОБЪЯВЛЕНИЙ��6   $��-3,0,0,0q�60   $��1,0,0,0q�6B   ��6\   Z� ��5�      �� 5�      $	 1,1&�6*   $	 0,1&�69   .��5�      � $	 	LightBlue��6'   $
 1Q�69   � 5�      $	 	#FF6C98CD��6'   � 5�      $	 	LightBlue��6'   $
 0.042Q�69   5�      .��5�      � .+�5�      ]� ��. .+�5�      �� $��Bottom=�6%   $ 
Horizontal=�6=   $	��100��6V   .+�5�      �� -   5        $��_messageToDB��5�   H   $��14(�6&   $
 Wrap=�64   $��150,20,0,20q�6\   $��Stretch=�6q   $	��550��6�   ���	#FFEAF7F9.��6�   5�        5�      -   5        $��SendMessageButton��5�   2   $��50��6K   $��50��6V   # �  6|   # �  6�   #���  6�       PressedBackground#  �  6  !   PressedBorderBrush! �	#FFA4A4A4"   KeyTip$"  ��6v  # �� 	IsDefault# . 6�  5�      �� 5�      -   5        $��	_messages��5�   "   $��100,20,100,0q�63   
������ ��.  #��[ 5�   "   ��5�      �� 5�   "   .R�5�   &   ]� ��. .+�5�   *   �� $��Bottom=�62    . $��0,0,0,15q�6\   $��18(�6n   $��	ComicSans)�5�   2   $
 Wrap=�6I   $	��600��6]   ��5�   2   �� $ MessageText�$ PropertyChanged=�$ TwoWay=�.��6�   ��5�   .   �� 5�   2   .R�5�   6   �� $ �� CornerRadius$$ 10��6=   % �� Padding$% 5q�6O   $��2q�6[   ���	#FF8F8F8F._�5�   :   �� & ��Name$& PART_ContentHost��6G   |�.  $��Hidden=�6s   $��Hidden=�6�   5�   7   5�   3   .��5�   @   5�   +   ]� 5�   *   $	��5,0q�64   .+�5�   .   �� $
��Left=�68   
������ $
��Auto��6g   ��6t   �� $ Student.Name�$ PropertyChanged=�$ TwoWay=�  5�   .   $��delMsg��6Y   -   5        $��Right=�5�   B   $ x��6z   �� 5�   .   
������ $
��Auto��6Q   $
��15,0q�6^   ��6l   �� $ MessageDate�$ PropertyChanged=�$ TwoWay=�5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      