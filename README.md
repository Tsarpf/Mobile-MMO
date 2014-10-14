Project RahatPois
=========

In finland we call it a rahasampo

####Finished features
#####All the following is implemented in at least some way in both backend and the client using connections that go through secure SSL/TLS sockets:
- New user registration
- Logging in/out
- Separation of users to rooms/areas/whatever
- Room join events etc.
- Room-wide chatting between users in the same room
- Room-wide moving around within the room's area/grid


### In short: 3D clone of a finnish well-known chatting "game". Targeting mobile.


Main features: 
- Client made with Unity3D, 3D graphics with camera locked to being isometric.
    * first only for mobile (at least android, hopefully ios too)
    * when unity 5 comes with desktop browser support without unity plugin, then support that too.
    * minigames, at first only easy ones (implement-wise) like rock-paper-scissors and bejeweled.
    * minimalistic user interactions (f.ex only four major directions to move in) so coding the server is easier and less error prone.
    * ...?
- NodeJS(?) backend to minimize coding and increase ~scalability~
  * Normal authing (st|fl)uff
  * Easily re-establishable socket connections so frequent disconnects because of mobile networks cause minimal problems in gameplay
  * Authoritative, clients can only request for actions, server responds with what actually happened (if anything)
  * ...?



Money stuff ideas:
- Having two kinds of virtual money creates more hype around the actual-real-money-costing stuff plus creates an obvious distinction between stuff acquirable with pure gameplay and stuff you have to pay for. The actual-real-money-costing stuff has to seem really luxurious (like custom T-shirts which are expensive for the server to handle so requiring money for them is justifiable) so the creator's of the game don't seem greedy  (Which is why for starters there probably shouldn't be any monthly subscription based stuff. But maybe later)
- Two kinds of virtual money, one that is purchasable with real money, and one that is earned with gameplay. F.ex 1 credit per minute playing, 10 credits per finished minigame? If multiplayer minigame, increase payouts by a multiplier to incentivize inviting friends to the game etc.
- Real-money to purchasable-money conversion chart idea encouraging buying a lot every time (many people probably buy only once ever?):
   * 1 eur == 5
   * 5 eur == 30
   * 10 eur == 75
   * 20 eur == 200
- Purchasable-money converts to credits something like 1 -> 1000 credits
- A simple chair could be something like 200 credits.
- A customizable T-shirt (20*20 pixel area with custom chosen colors imprinted on avatar's shirt) etc. luxury items could be like 5 purchasable coins and not purchasable with credits at all.

Purchasable stuff:
- Purchasing interior decoration with virtual money
   * By grinding minigames you can get approximately 1 item, like a chair per 1-2 hours -> decorating an entire room requires hours and hours of playing.
   * If you get the credits through buying purchasable-money for 10 eur, you can get a room full of stuff in just a few minutes
