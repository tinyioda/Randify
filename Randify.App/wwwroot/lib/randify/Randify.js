window.onSpotifyWebPlaybackSDKReady = () => {
    console.log("The Web Playback SDK is ready. We have access to Spotify.Player");
    console.log(window.Spotify.Player);
};

Blazor.registerFunction('deleteAllCookies', () => {
    var cookies = document.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
});

Blazor.registerFunction('enableSpotifyPlayer', (t, uri) => {
    player = new Spotify.Player({ name: 'Randify!', getOAuthToken: cb => { cb(token); } });
    token = t;

    // Error handling
    player.addListener('initialization_error', ({ message }) => { console.error(message); });
    player.addListener('authentication_error', ({ message }) => { console.error(message); });
    player.addListener('account_error', ({ message }) => { console.error(message); });
    player.addListener('playback_error', ({ message }) => { console.error(message); });

    // Playback status updates
    player.addListener('player_state_changed', state => { console.log(state); );

    // Ready
    player.addListener('ready', ({ device_id }) => { console.log('Ready with Device ID', device_id); deviceId = device_id; });

    // Not Ready
    player.addListener('not_ready', ({ device_id }) => { console.log('Device ID has gone offline', device_id); });

    // Connect to the player!
    player.connect();
});

Blazor.registerFunction('play', (uri) => {
    console.log("asking to play: " + uri);

    fetch('https://api.spotify.com/v1/me/player/play?device_id=' + deviceId, {
        method: 'PUT',
        body: JSON.stringify({ uris: [uri] }),
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        },
    });
});