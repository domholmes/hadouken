var Healer = function (player) {

    this.player = player;
    this.potionIsCoolingDown = false;
    this.potionCoolDownTimeSeconds = 30;
    this.potionHealPercent = 60;

    this.healForDuration = function (duration) {

        var lifeGain = (this.player.lifePerSecond / 1000) * duration;

        this.player.life += lifeGain;

        this.tryHealWithPotion();

        this.checkMaxHealth();
    }

    this.healForHit = function () {

        this.player.life += this.player.lifeOnHit;

        this.checkMaxHealth();
    }

    this.tryHealWithPotion = function () {

        if (!this.potionIsCoolingDown && this.playerShouldUsePotion()) {

            this.player.life += this.player.life * this.potionHealPercent;
            this.potionIsCoolingDown = true;
        }
    }

    this.playerShouldUsePotion = function () {

        return player.life <= player.maxLife * (this.potionHealPercent / 100);
    }

    this.checkMaxHealth = function () {

        if (this.player.life > this.player.maxLife) {

            this.player.life = this.player.maxLife;
        }
    }
}