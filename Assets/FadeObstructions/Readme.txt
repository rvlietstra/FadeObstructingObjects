=== Fade Obstructions ===

If you need any support you can contact me at: reyn.vlietstra@gmail.com

=== Setup ===

1. Add the FadeObstructingObjects script to your player.
2. Set the Fade Shader to the supplied DiffuseZWrite shader.
3. Set the Camera to the camera that will be viewing the player.

=== Options ===

Seconds: The number of seconds it takes for the fading effect to occur
Ray radius: The ray radius is the radius of the ray used to find the obstructing objects
Final Alpha: The transparency level, 0 being completely transparent, 1 being competely opaque
Layer Mask: All objects on the selected layers will be faded if found to obstruct the player

=== Overriding defaults ===

At some point you will have an object that you want to handle differently than 
the defaults defined in the FadeObstructingObjects script placed on the player.

You can add the FadeObjectOptions script to any object and override those values.

Seconds: override the default seconds, a value of -1 will use the above defaults.
Final Alpha: Override the default final alpha -1 will use the above defaults
Fade Shader: Override the default transparency shader, empty will use the above defaults


