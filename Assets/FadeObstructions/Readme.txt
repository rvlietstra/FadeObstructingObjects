=== Fade Obstructions ===

If you need any support you can contact me at: reyn.vlietstra@gmail.com

Please have a look at the tutorial video at: https://www.youtube.com/watch?v=CmoL7ZAMFyI

=== Setup ===

1. Make sure the objects that will fade have their shader's rendering mode set to 'Fade'
2. Add the FadeObstructionsManager script to any GameObject and specify your camera on it
3. Add the FadeToMe script to your player

=== Options ===

Final Alpha: The transparency level, 0 being completely transparent, 1 being competely opaque
Fade out seconds: The number of seconds it takes for the objects to fade out
Fade in seconds: The number of seconds it takes for the objects to fade back to full opacity
Radius: The ray radius is the radius of the ray used to find the obstructing objects
Layer Mask: All objects on the selected layers will be faded if found to obstruct the player

=== Overriding defaults ===

At some point you will have an object that you want to handle differently than 
the defaults defined in the FadeObstructingObjects script placed on the player.

You can add the FadeObjectOptions script to any object and override those values.



