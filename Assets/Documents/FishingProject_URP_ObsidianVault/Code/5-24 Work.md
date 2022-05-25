# And We're Back
That's right, back at these joints again. How fun, and crazy. At first, I  wasn't really sure about how these Rigidbodies should work with my system, but I think I'm getting it more now. In any case, I'm going to make my attempts at recreating the fishing rod in the actual game project right now. Going in headfirst is always fun, if not a little scary.

First problem though. I can't parent Rigidbodies and expect them to act nicely. So how can I get the fishing rod in the player's hand for one?

Now I have to think about how to activate and deactivate this. Interesting!

Oogh but now I worry that I may be straying from best practices (which is cool) but at the same time may lead to unpredictable results. I wanted to make some script to assemble and simply pool instances of EquippableGear for use in the player's hand. I want multiple things to be able to be held, so I'd have to pool at least one instance of them depending on what it is.
	Thinking about it for more than a second, I should only really need one front-end-y prefab to load for each one. For example, I'm thinking about traps, I don't want to depool then place a trap and then have to make and pool a new one. But that's for later I think, I'm focused on an EquippableGear that's really dynamic right now with the fishing rod.

---

Thinking a bit deeper about this yet, I don't really care too much about the Rod mesh. I can have a root-level \_Base-prefix GameObject that the mesh is parented to. This should probably have the Animator and things like that.

But after that, I have to make the Bobber which is the most important part of casting. I have to move it to some standard point on the \_Base-prefix GameObject and activate the FixedJoint immediately afterwards (probably making it kinematic as well). **From what I understand right now, the Bobber is the only thing that really NEEDS to be simulated.** 

The intermediary that I'm having trouble with is the little Line joints in-between that I'll draw as cables. They matter (at least for polish), but I worry they'll interfere too much with my process. I think I can figure them out, but it migiht be time-expensive. But who am I kidding, this whole process and solution is a little time-expensive.

Again, the bobber and some point at the tip of the fishing rod might be the only things that "NEED" need to be simulated through physics and jointery. There's a chance that later I can add my line the way I was thinking, or some other way.

So yeah, I think I have to come up with some kinematic Rigidbody at a point on the FishingRod mesh that I can use to pull the Bobber too and potentially attach Line GameObjects to.

...Yes, then I need only kinematize (?) the Bobber as I need and don't need, just like Unity says for Ragdoll stuff (and the lot of folks whose forum questions I've read).

---

Ah-ha! I think I've got it now! So I don't really need any non-kinematic Rigidbodies on the base GameObject with the fishing rod mesh. That one's used for animations and things like that. The only part that's simulated and actually goes out and does its thing is the bobber at the moment. That's great, because that means I don't have to do anything super crazy at the moment.

... I think it's working! I think I've figured that portion of it out so far. Now I'm concerned with actually generating and connecting my Bobber to my FishingRod GameObject.

Yeah, now that I've figured out a way to organize this a *little* more, my whole thing works pretty much just like it did before, but better thankfully.

However, I'm not done with my stuff just yet. I need to figure out how to work with my Bobber when its not in play or active. That likely involves some kinematizing and things like that.

As well, I'm worrying about how I want to get spawning this in done (and loading the fishing rod at a position as not to break Joints and stuff like that). As well, if its possible to generate a bobber at runtime or not (or if I need to) For now, I'm just going to try and focus on this.

---

One thing that would help currently (or after I complete this step) AS WELL is to make the REELING velocity something that caps out at a constant rate when the total velocity vector is closer to the distance vector from the bobber to the line base. This will likely have me using the dot product of the velocity vector to compare against the distance vector. What an unfriendly paragraph to read, LOL!

For now, though, I have to consider how to work with my Bobber to attach and detach the bobber as I want to.

(I may  or may not also be considering about the FishingLine Rigidbodies...)

