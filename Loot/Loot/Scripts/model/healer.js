var Healer = function (player) {

    this.player = player;

    this.healForDuration = function (duration) {

        var lifeGain = (this.player.lifePerSecond / 1000) * duration;

        this.player.life += lifeGain;

        if (this.player.life > this.player.maxLife) {

            this.player.life = this.player.maxLife;
        }
    }

    this.healForHit = function () {

        this.player.life += this.player.lifeOnHit;

        if (this.player.life > this.player.maxLife) {

            this.player.life = this.player.maxLife;
        }
    }
}