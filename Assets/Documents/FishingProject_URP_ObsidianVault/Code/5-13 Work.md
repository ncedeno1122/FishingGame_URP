I don't have a lot for life updates right now really besides the fact that I'm enjoying being a bit lazy in summer. But, I do want to continue working on this game, and so, here I am. :D
Let's get some good work done today!

## Where I Left Off
My previous note, [[5-4 Work]], detailed my approach to try and create FishingAreas that could support my bobbers and things like that. What I was struggling before with was a way to float the bobber without getting it to get bounced above the surface, which could be done in a number of ways.
Before I go over the cool new idea I thought of just now, I definitely think that there needs to be some fix to the way that I'm floating the bobber, it bothers me that it's not right yet. That said, my new way may prove to be what I need.

#### De-Registering the bobber NOT just by collision, but by input?

So currently I have my Bobber, when cast, colliding with a FishingArea's  trigger collider and pretty much fahgedding-about-it past that point. The FishingArea maintains a list of fishing sessions that will be used for analytics and other stuff, but one major thing I think that's important for this right now is **de-registering the Bobber without collision**.

This sounds weird how I'm saying it, but I think if I have some sort of way to register the Bobber to the fishing area I hit OnTriggerEnter, create a FishingAreaSession, and **ONLY close the session** when the Bobber is no longer active, or being reeled in, I get what I'm looking for.

#### Parenting the Bobber to the FishingArea and "babysitting it?"
What if, OnTriggerEnter, we parented the Bobber to the FishingArea, and were able to create some parent GameObject that "babysat" the Bobber by working out the floating/bobbing animation, or something?
This approach sounds useful, but I don't know if that's really what I want.

#### Doing the math better
Alright, so here's a concept LOL! Right now, the problem I'm having with floatation that's messing me up is that I can't apply more upwards force than the weight of the object as it approaches the shallow depths of the water.

That's the part that no equation of mine has factored in yet: depth. It's not necessarily what I'm dreading, but it will be useful in applying a consistent upwards force to the bobber. This also may allow me, with a constant force, to modify the mass of the bobber and potential tackle (weights) to deliberately attract deeper-dwelling fish.

In any case, I think what needs to happen is that I find an equation that I can plug the Bobber's current depth at any point, and calculate a force that is slightly greater than the force of gravity the deeper we are, and eventually goes negative some distance from the surface.
It definitely sounds like the function I'm looking for is piecewise, and could even be defined by a parabola that's slightly offset on the positive x-axis and negative y-axis.

Right now I'm looking at the standard-form parabola $$ax^2+bx+c$$ and it seems that for a=1, b=0, and c=-1, I get something very simple that describes the behavior I want. As the input value for depth approaches 0, we dip down to -1. This may have the effect of actually finding a point of equilibrium, so I may have to apply a more drastic force with a piecewise function or something, but let me try this with my current approach. Provided this works, I wouldn't have to change my current approach of floating the bobber and whatnot. I'm not backing down, but I should aim for efficiency.

---

Alrighty, after a little bit of mathing, I think I found something that works. The quadratic equation is nice, but I'm still getting a big bounce. Looks like I'll just need to change around some values and I'll be good.

I am right though, that an equilibrium point will be found.

Basically, the 'x' variable into the quadratic equation is the normalized depth *from the surface*. This allows me to use points between 0 and 1 on the graph and the x-axis as depth from the surface to target specific force values I want to apply to the Bobber. Presently, this allows the Bobber to hover around a specific depth that I can change by modifying the a, b, and c values of the parabola. I bet I've made all my math teachers proud (and weep at my misuse of these amazing concepts).

I have to remember that the graph is a force graph of the applied force from the distance from the surface of the water and I'll be good.
For now, I'll stop tweaking with values and continue to develop the whole fishing operation.

**Next step, reeling**.

---

Well, the basic concept of reeling was almost more animation than it was scripting. One little state called `FishingRodReeling` and the basic concept was done with.

I do need to implement a maximum line length thing so that you can't just have infinite line or something. It'd have to essentially apply the force to reel but maybe a little harder if the distance exceeds some constant line length.

## ALRIGHTY
Well, I've hit a new roadblock referring to how I'm actually using a bobber. This was to be expected in the way that I return my nice little Bobber Rigidbody to its original position. In any case, here's the error I'm encountering:
	When I unparent the Bobber and "detach" it (turn off kinematics, nullify its parent), things are fine. When it comes time to "reattach" the object (the same process in reverse, in that order), the scale, rotation, and position of the Bobber get quite variably distorted.
Originally I thought this was for some reason relating to the Rigidbody overriding Transform data like position and all that stuff.

***Now, it seems that I might need to take a look at Joints***. It's been a long time coming, and I'm glad I get to learn about them here. How exciting! But not without a little snack. It's 2:17am right now and I have things to do later tomorrow, so I may or may not resume work at this point.

Yeah, I think I'll take care of that tomorrow. Man, for only four hours, I got a decent bit of stuff done. Here's to more happy developing!