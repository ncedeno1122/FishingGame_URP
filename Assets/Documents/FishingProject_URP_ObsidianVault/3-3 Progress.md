# OH YEAH BABY
That's right, I was absolutely right in [[3-2 Progress]] when I said I'd be cruising as I defined some of the States. A lot of my work today was on fleshing a few of those states out, ironing the transitions between them out, and now working on launching the Rigidbody.

I have to decide whether I want to simulate the Rigidbody to work on things like the Wind Zone comonent (assuming it applies some constant force on Rigidbodies in the zone), or not. Essentially, I'm deciding **whether or not I want the Rigidbody for the Bobber to be kinematic or not.**
The advantages of a kinematic Rigidbody bobber launch is that I will define the launch pattern. That is to say, I can calculate the trajectory of the bobber, checking for collisions and where using physics calculations. This can allow me to earlier detect issues with casts, and prevent them from happening. **This approach offers a lot more control at the cost of some potential brain ouchies as I try to remember 2D kinematic equations**.
Comparatively, there's not a lot of advantages I can think of for a non-kinematic Rigidbody besides simplicity and lack of control, neither of which I particularly like. For now, perhaps, ***I'll elect for the kinematic approach***.
Ok I'm going to the arcade now with my sister :)

---

WOW the arcade was quite fun as usual. Perhaps I'll make a rhythm game some day, might be fun to figure out organizing and generating song data. I wonder what might be more palatable for that, JSON or XML. Probably JSON. What's more, I wonder how easy it might be to make an editor to create tracks within it...

In any case, I'll resume some work tonight. Tomorrow I have a breakfast to attend to.

I'll now have to figure out some of the equations I need. Good old kinematic equations, you've got to love them! As I recall, each kinematic equation was derived with respect to a missing variable, so I'll see which one suits me here.

***Dude look at this MathJax notation... Obsidian is so good to me...***
Equation 1: Missing $\Delta x$
$$v_1 = v_0 + at$$
Equation 2: Missing $a$
$$\Delta x = t(\frac{v_1+v_0}{2})$$
Equation 3:  Missing $v_1$
$$\Delta x = v_0t + \frac{1}{2}at^2$$
Equation 4: Missing $t$ 
$$v_1^2 = v_0^2 + 2a\Delta x$$
Very well. Well, here's my constants and what I know. I know there should be some constant $v_0$ speed, I know that $v_1$ should be 0, I know the acceleration value $a$, but I don't know $\Delta x$. I also don't know $t$...
*I also can't forget that I need two of these equations, one for each dimension*.
Alright, so I'm fairly sure that I need to solve for $\Delta x$ more than I need $t$ by a longshot. So, I'll take equation 4 for the vertical equation, and I'll need to rearrange it to solve for my $\Delta x$. That'll should look like:
$$\Delta x = \frac{v_1^2-v_0^2}{2a}$$
***IF*** I don't suffer from stinky syndrome and remember my algebra properly.

Wait I'm tying myself up mentally, I'm confusing myself. What I'm trying to locate is the point at which the bobber hits the ground in front of the player. I have to think about how each axis moves... AND the role that the power of the swing (0f-1f) will affect this...
WAIT I'm right... So I do actually know $\Delta x$, because that'll be some constant maximum distance scaled by the power of the swing from the Precasting state. When I was running this through in my head, the lack of a known $t$ value was really messing with me! Perhaps the power of the swing can help scale the $v_0$ values for each axis.
**WAIT AGAIN**, but I can have a maximum known distance and confirmed $\Delta x$ on the horizontal equation, but I DON'T know the $\Delta y$ for the vertical equation. If I solve for $t$ on the horizontal equation first, I can use that value for the vertical to solve for the $\Delta y$ value...

Alright, so that seems to make some sense. In the horizontal equation, I know the initial and final velocity from the cast. I'll know the displacement value because it's the scaled result of the power of the swing and the maximum cast distance. *This will allow me to find my time value*, because there's no relevant acceleration value at the moment, it's a constant on the horizontal axis (currently, when I'm not accounting for wind).

The time value will allow me to work with my second vertical equation, which I can use to find the total vertical displacement. *I actually don't think I'd need $v_1$ for this equation??* So I could use Equation 3 for the vertical displacement and all that...

My goal for these equations is to give me a series of lines that I can use to lerp the position of the bobber to, eventually stopping at the point of collision that I can check using a few test points.

The key here is a good, long time value if I have one defined or something... Truthfully, I'm interested in an equation that can continue for longer and longer time values to account for really long cliff fishing or something like that, I'm thinking. Truthfully, I just need to get these  calculations out of my head and into the computer. I'll give it a go!

Update: transcribing the calculations into general kinematic equation functions gave me more insight into what they do, but not ENOUGH to feel fully confident implementing them. I had a micro catharsis understanding what I was looking for vertically, but I just need to double check my logic. I'll draw it out.

Ok so I think I REALLY get it now. Basically, I'm hesitant around the time value because it's my independent value for the equations. For the horizontal equation I don't need to know acceleration, it's simply a constant, and I'll need to find out
WAIT
Ok, I'm tying myself in circles because I *think* these equations can be fairly forgiving about the values they return. I'm bugging out because I don't know what I want as the variable I want to control to get test point values, time or deltaX. But when I get down to it, I think I can do either. My master plan is to create these equations, then use a controlled variable to create a series of test points. I'll use the physics casting system to check the areas of each point to see if I hit something somewhere in the arc. If it's something I want to hit, like a water area hitbox, then I'll allow the swing. If not, then I want nothing to do with it. 
Thus, I don't really need to know the deltaX or deltaY for either equation, which is the main thing. If I create sufficient test points with the manipulating of time to check, then I SHOULD be gucci.

For the horizontal equation then, I can use something really easy like Equation 2, which gives me the deltaX without the acceleration hassle.

For the vertical equation, I'll need to find deltaY, but I don't know my final velocity and I do know my acceleration. Sounds like Equation 3.

That sounds like it makes sense. Perhaps I'll try to visualize it. My brain is pumping, I feel like those nasty Baby Yoshis from that one Mario game, you know the picture.

Now I just need to use these equations and combine them with the 3D space, likely with `TransformPoint()` stuff.

This happens to be the current thing I've been struggling with for a moment. I have an initial attempt that worked horizontally but NOT vertically, now I'm reworking my approach but I need to figure out how to turn my 1-dimensional horizontal offset point into a 2-dimensional one based on the forward vector.

---

After quite a bit of fumbling around, I've GOT it (at 3:29am `:,]`)! Now I've created a set of functions that helps me calculate the trajectory of the cast relative to a transform position and direction. I can even get the exact point of collision!

Now, I have some more design decisions to make. Good games let you do stuff even if it's stupid. If there's a tree in the trajectory, should we allow the cast? If not, I'd imagine it could be frustrating.

However, I'm even getting ahead of myself there... How will I even launch the bobber, much less reel it in?
The action of reeling is generally drawing a triangle's hypotenuse between the tip of the rod and the bobber. A force vector, changed by gravity, would pull the bobber towards the tip of the rod. This, if my bobber rigidbody wasn't kinematic, would be mega easy funtimes. However, this may not be mega-easy funtimes if I can't figure it out. 

This is where the main clash of ideals from earlier is involved. If I'm using a kinematic bobber, then I need to run the calculations and do things by the code. That means, however, that I might not be able to simulate what I want to with the bobber, like a much simpler 'reeling' mechanic than what I'm having in mind right now.

That said, do I really 'need' to reel? In the lore of my game, you're a soul collector spirit, and your rod will gather the soul of the fish. The soul leaves the body, but what response does that have? I might not have to actually reel in the fish like a Cabela's fishing game or something like that...

**Instead, I can use some sort of minigame, or other thing to reel.** Phew! Thank goodness again for scoping things out decently.

If I can avoid simulating reeling, I absolutely will. However, I haven't decided if I want to let the Player cast if they hit something that's not a fishing area. In that case I might have to simulate the bobber hitting the ground or something, but I can instantly 'reel' it back in. If it's a fishing area, UNTIL a fish appears (somehow) and that whole event occurs, you should be able to do some reeling action in the same way. This would instantly pull the bobber back to its original position, from wherever it landed. That's just what I need to simulate next.

Or tomorrow, for that matter, I'll have to simulate it tomorrow. Overall, it was a fantastic night of work, time to let myself rest.