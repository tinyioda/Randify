window.onSpotifyWebPlaybackSDKReady = () => {
    console.log("The Web Playback SDK is ready. We have access to Spotify.Player");
    console.log(window.Spotify.Player);
};

window.RandifyJS = {
    showPrompt: function (message) {
        return prompt(message, 'Type anything here');
    },

    deleteAllCookies: function () {
        var cookies = document.cookie.split(";");

        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i];
            var eqPos = cookie.indexOf("=");
            var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }
    },

    enableSpotifyPlayer: function (t, uri) {
        player = new Spotify.Player({ name: 'Randify!', getOAuthToken: cb => { cb(token); } });
        token = t;

        // Error handling
        player.addListener('initialization_error', ({ message }) => { console.error(message); });
        player.addListener('authentication_error', ({ message }) => { console.error(message); });
        player.addListener('account_error', ({ message }) => { console.error(message); });
        player.addListener('playback_error', ({ message }) => { console.error(message); });

        // Playback status updates
        player.addListener('player_state_changed', state => {
            console.log(state);

            const assemblyName = 'Randify';
            const namespace = 'Randify.Services';
            const typeName = 'SpotifyService';
            const methodName = 'PlayerStateChange';

            const method = Blazor.platform.findMethod(
                assemblyName,
                namespace,
                typeName,
                methodName
            );

            let messageAsDotNetString = Blazor.platform.toDotNetString(JSON.stringify(state));

            let result = Blazor.platform.callMethod(method, null, [messageAsDotNetString]);
        });

        // Ready
        player.addListener('ready', ({ device_id }) => { console.log('Ready with Device ID', device_id); deviceId = device_id; });

        // Not Ready
        player.addListener('not_ready', ({ device_id }) => { console.log('Device ID has gone offline', device_id); });

        // Connect to the player!
        return player.connect();
    },

    togglePlay: function () {
        player.togglePlay();
    },

    play: function (uri) {
        console.log("asking to play: " + uri);
        console.log("on device id: " + deviceId);
        console.log("using token: " + token);

        fetch('https://api.spotify.com/v1/me/player/play?device_id=' + deviceId, {
            method: 'PUT',
            body: JSON.stringify({ uris: [uri] }),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
        }).then(function (response) {
            return response.status;
        });;
    }
};