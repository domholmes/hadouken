var Battle = function (p1stats, p2stats) {

    var self = this;

    this.status = "";
    this.startTime;
    this.intervalId;
    this.intervalLength = 16.66;

    this.p1 = new Player(p1stats);
    this.p2 = new Player(p2stats);

    this.p1.opponent = this.p2;
    this.p2.opponent = this.p1;

    this.begin = function()
    {
        self.startTime = new Date().getTime() / 1000;

        self.act();
        self.intervalId = window.setInterval(self.act, self.intervalLength);
    }

    this.act = function(){

        self.p1.attack();
        self.checkDead();
        self.p1.heal(self.intervalLength);

        self.p2.attack();
        self.checkDead();
        
    }

    this.checkDead = function () {

        if (!self.p1.isAlive() || !self.p2.isAlive()) {

            var endTime = new Date().getTime() / 1000;
            self.status = "Battle lasted " + (endTime - self.startTime).toFixed(2) + " seconds"
            window.clearInterval(self.intervalId);
        }
    }
}