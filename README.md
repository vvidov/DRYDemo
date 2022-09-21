# Welcome to my Playground!

Hi! The Idea of that repository is to show that old .net Framework like 4.7, WinForms and WPF can have a "modern" software  and manageable design. As prove of the concept and try a new things.

For staring I use point is a [DRYDemo](https://www.youtube.com/watch?v=dhnsegiPXoo) from [Tim Corey](https://www.iamtimcorey.com/). Thank you Tim, for free Videos and good examples, as you see at least one in use. Keep going:) that ist not an Ads. That Video and some others made me to create that place.

For me was not enough to have a small demo with one processor so I created a new one. Later I added [Autofac Container](https://autofac.org/) to it. So I can config my App by code(WPF app) and Json Config file(WinForms).
With Autofac I have created LoggerInterceptor so I can easy extend my methods with Logging. I can activate it or switch between different loggers only by changing config container. I can change also starting point of WinForm app, I can choose between 4 Forms, 2 processors(please, forgive me the second one has not unit tests), I can activate Logging and select one of 3 implementations(the last one is on the Moon, sorry clouds are not too high as my goals, and not so stable to hold my logging:))
Next I added  MVVM:) and [ReactiveUI](https://www.reactiveui.net/) to apps. My Veiws contains only binding and ModelView. There is one more method to display messages to user and introduce some interaction with him. There are Unit test for ViewModel and I can test everything(all business logic is in VM). For Model I have implemented 2 file storage(Json and XML). I use the same ViewModel also for WPF app(also contains only bindings, and VM and user DisplayMessage, no code behind:)). To all that I can config 2 storages for Models.
I can also extend any of 2 app with other types of logging, storage, processors and views, without touching exiting code. I just have to implement new code, test it with unit tests, if it is ready, I can add it to config container and make a new configuration. That is it. 

When to expect to be updated with new things. That is the question. That is playground and will stay so.
If I have time, mood and an idea I will try to make it:)

What will come next:
 - convert View to control and use it in other windows and show
   master detail content 
 - add some modern design to Winforms and WPF
 - try to use the same ViewModel in another app stack
 - improve and update constantly document:)
 
 There a some many thing to play with. 
 As we see  software and world are full of opportunities. And we must take any of them. 
 Do not forget, that an error is always an option. That you can explore it and learn a lot.
 I do not like to reinvent the wheel, but I enjoy riding the bicycle.

Keep playing:)
