   M S B A M L   `   `   `  ���� D  @Organizer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Organizer	Organizer  %clr-namespace:Organizer	Organizer      Organizer.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:Organizer     MCustom@http://schemas.microsoft.com/winfx/2006/xaml/presentation/ribbon  )databindingclr-namespace:Organizer  -     �Name$  MainWindow1�� � Title$ 	Organizer��$	��597��$	��934��$	��934��$	��597��$	��597��$	��934��
���   �
���   � � Icon$- '/Organizer;component/Resources/icon.ico�� � 
ResizeMode$ CanMinimize=���Z�  � 	Resources    Organizer.DayConverter   DayConverter   Organizer.LessonConverter  LessonConverter&    Organizer.ProgressStateConverter  ProgressStateConverter  _addNoteImage �� ImageSource  _minusImage  
_plusImage  _sendMessageImage��%  &	        &	      &	 
     &	      &	 =     &	 f     &	 �           �� $( "@..\..\..\Resources\addNotePic.png���� $# @..\..\..\Resources\minus.png���� $" @..\..\..\Resources\plus.png���� $) #@..\..\..\Resources\sendMessage.png��.��� $��0q�����  �� EndPoint$ 0.5,1&� � MappingMode$ RelativeToBoundingBox=� �� 
StartPoint$ 0.5,0&�.��� 	 � Color$	 	#FF5DEEE7��� $	 	#FF00A2FF��
 � Offset$
 1Q�.+��� -   $��MainTabControl��$��20,10,30,10q����	#FFDAFAFF$��0q���Z� .@��� -   $��ProgressTabItem��$��ПРОГРЕСС��$��30��$
��Auto��$��-3,0,0,0q�
����ح�$��Stretch=�$��1,0,0,0q���Z� .��]� ��. .+��� $
��0,20q�$	��Top=� �� Orientation$ 
Horizontal=�$��50��.+��� -   $��_lessonsBox��$	��250��$��24(�4  .Microsoft.Windows.Controls.Ribbon.RibbonButton  -   $��50��#��[    FocusedBackground# �     MouseOverBackground# �  �� -   $"��NotificationMessgeToProgress��$��Papyrus)�$	��550��$��Right=�$��Center=�$��20(����	#FFF32E2E�� -   $��_progressList��
������   ListBoxNews#��[ ���� .R�� $	��750��.+�]� ��. $	��5,0q�.+��� $��Right=�$ Vertical=�.+��� $ 
Horizontal=�.+��� $	��100��$��Всего заданий���� $��30��$��Center=����� *NeededTasks+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� .+�  -   $��15��$��15��   Label$ +�����	#FFA4A5A6  -	   $��15��$��15��$ -�����	#FFA4A5A6  -
   $��24��$��20(�$ x���� $ 
Horizontal=�.+��� $	��100��$��Сдано заданий���� $��30��$��Center=����� *CompletedTasks+$ TwoWay=�$ PropertyChanged=��� $ Vertical=�.+�  -   $��15��$��15�����	#FFA4A5A6$ +��  -   $��15��$��15�����	#FFA4A5A6$ -���� .+��� $��Center=����� *
LessonName+�  � Minimum$ 0Q� � Maximum$	 100Q�$��30�� � Value �� *TaskProgress+$ TwoWay=�$ PropertyChanged=� �� 	Converter# [ �� $��Заметки��$��-3,0,0,0q�$��1,0,0,0q���Z� ���� $	 1,1&�$	 0,1&�.��� $	 	LightBlue��$
 1Q�� $	 	#FF6C98CD��� $	 	LightBlue��$
 0.042Q�.���  � ColumnDefinitions �� $��* ��� $��* �.+�]� ��. $��1��.+��� -   $��	_noteText��$	��300��$	��400�� �� TextWrapping$
 Wrap=� J� IsManipulationEnabled . $��Segoe Print)�$��14(����	#FFFBF690� $��0��.+��� .+�&   System.Windows.Controls.Calendar  -   $��	_calendar��   SelectionMode$ MultipleRange=��� $ 
Horizontal=�.+�  -   $��AddNoteButton��$��20��$ Добавить��$��
140,0,50,0q� �	#7F3EF121  -   $��DeleteNoteButton��$��20��$ Удалить�� �	#7FF51C1C�� $
��Left=�$
��20,0q�$��16(�$-��'Существующие заметки���� -   $��ExistingNotesList��$
��20,0q�$	��245������ .R��� $ 
Horizontal=�$	��5,0q�.+��� $	��100������  �� Path$ NoteDate��� $	��200������ $ NoteDescription��� $��РАСПИСАНИЕ��$��-3,0,0,0q�$��1,0,0,0q���Z� ���� $	 1,1&�$	 0,1&�.��� $	 	LightBlue��$
 1Q�� $	 	#FF6C98CD��� $	 	LightBlue��$
 0.042Q�.��� .+�]� ��. .+��� $ 
Horizontal=�$	��Top=� � DataContext �� *SelectedTimeTable+.+���  �� 
IsReadOnly . $��Center=�$	��100�� � Cursor$ Arrow������ *Day+# [  $ 	LostFocus=�$ TwoWay=���  . $��Center=�$��70��$ Arrow������ *LessonNumber+# [ $ 	LostFocus=�$ TwoWay=��� $��10,0,0,0q�$��Center=�$��Аудитория���� -   $��5,0,15,0q�$��Center=�$��70������ *
Auditorium+$ 	LostFocus=�$ TwoWay=��� $��Center=�$��Тип���� -   $��5,0,15,0q�$��Center=�$��50������ *
LessonType+$ 	LostFocus=�$ TwoWay=��� $��Center=�$��Предмет���� -   $��5,0,15,0q�$��Center=�$	��200������ *
LessonName+$ 	LostFocus=�$ TwoWay=��� $��Center=�$!��Учебная неделя���� -   $��_week��$	��5,0q�.@��� $��Первая���� .��Вторая�� -   $��_lessons��  ListBoxLessons#��[ $��0q�$��Right=�$	��500��$��Disabled=���.  $��Disabled=� �� SelectedItem �� *SelectedTimeTable+��Z� ���� .R��� $ 
Horizontal=�$��0q�$��16�� �� CanVerticallyScroll .   J� OpacityMask Z� .+��� $��10,0,0,0q�$��70������ $ LessonNumber�# [ $ PropertyChanged=�$ TwoWay=��� $��50������ $ 
Auditorium�$ PropertyChanged=�$ TwoWay=��� $��30������ $ 
LessonType�$ PropertyChanged=�$ TwoWay=��� $	��300������ $ 
LessonName�$ PropertyChanged=�$ TwoWay=��� $ Vertical=�.+��� $��81��$��Понедельник��$��30(�$��Center=����	#FFF7F5F5�� $��81��$��Вторник��$��30(�$��Center=����	#FFF5E8DE�� $��81��$��
Среда��$��30(�$��Center=����	#FFF1F183�� $��81��$��Четверг��$��30(�$��Center=����	#FFF3F351�� $��81��$��Пятница��$��30(�$��Center=����	#FFFFFF2E�� $��81��$��Суббота��$��30(�$��Center=����	#FFC5F71B�� $%��ДОСКА ОБЪЯВЛЕНИЙ��$��-3,0,0,0q�$��1,0,0,0q���Z� ���� $	 1,1&�$	 0,1&�.��� $	 	LightBlue��$
 1Q�� $	 	#FF6C98CD��� $	 	LightBlue��$
 0.042Q�.��� .+�]� ��. .+��� $��Bottom=�$ 
Horizontal=�$	��100��.+��� -   $��_messageToDB��$��14(�$
 Wrap=�$��150,20,0,20q�$��Stretch=�$	��550�����	#FFEAF7F9.��  -   $��SendMessageButton��$��50��$��50��# �  # �  #���      PressedBackground#  �  !   PressedBorderBrush! �	#FFA4A4A4"   KeyTip$"  ��# �� 	IsDefault# . �� -   $��	_messages��$��100,20,100,0q�
������ ��.  #��[ ���� .R�]� ��. .+��� $��Bottom=� . $��0,0,0,15q�$��18(�$��	ComicSans)�$
 Wrap=�$	��600������ $ MessageText�$ PropertyChanged=�$ TwoWay=�.������ .R��� $ �� CornerRadius$$ 10��% �� Padding$% 5q�$��2q����	#FF8F8F8F._��� & ��Name$& PART_ContentHost��|�.  $��Hidden=�$��Hidden=�.��]� $	��5,0q�.+��� $
��Left=�
������ $
��Auto������ $ Student.Name�$ PropertyChanged=�$ TwoWay=�  $��delMsg��-   $��Right=�$ x���� 
������ $
��Auto��$
��15,0q����� $ MessageDate�$ PropertyChanged=�$ TwoWay=�