# VR-Project

## Abstract
The topic of the project was to develop a mixed reality application, utilising a large projecton area as output and speech recognition as input. By this means an application was to be build, which enhances the cocktailmixing experience.

An instructional interactive helper software was build, which teaches the user how to mix a cocktail from a pool of prepared recipes. A beamer projects a list of available cocktails onto a table, from which the user can choose the prefered recipe. The input is given via speech, by saying the number of the respective item.

The application is projected via a large projector onto a table, which is (ideally) white, or covered in white cloth, in order to assure visability of the projection. It is highly discouraged to use a table with a dark surface color. The application is built in a dark background color theme with white lettering ontop, since this further improves the visability of the projection.

The user is first presented a selection menu, where each available recipe is presented with a representative picture and a number. The user can then selected the cocktail by saying the respective number displayed next to it. After a recipe has been selected the user will be navigated through the preparation, in which the needed ingredients and tools will be displayed on the table, with a prompt telling the user to place the items on the highlighted area with the respective lable. The user is instructed to say *Next* when all preparations are ready.

Afterwards the first recipe step will be displayed. All ingredients and tools needed for this step will be highlighted by the projector. A text prompt will describe the requiered action the user has to perform with the highlighted ingredients, e.g. *Pour juice into cocktail glass. Add ice to the glass*. To visually aid how the task is performed, a video plays in the top right corner, showing the action described in the text instruction. The video will loop until the step is completed. Once the user has successfully performed the instructed task, he will need to again place the ingredients and tools onto the labled position. In order to continue to the next step, the user is prompted to say *Next*, which will lead to the step in the recipe until the cocktail is finished, where the user is presented with a final screen.

The user is able to access the previous step by saying *Previous*, until the first step is reached. At any time in the application the user can return to the selection screen. This can be achieved by saying *Exit*.  

![alt text](https://github.com/GeibTobias/Making-Virtual-and-Augmented-Reality-great-again---Project/blob/master/cocktailMixer.PNG "Setup Screen")
