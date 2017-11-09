$(function () {
    var app = new Vue({
        el: '#app',
        data: {
            clients: [],
            selectedClient: null,
            recentOrders: []
        },
        methods: {
            showRecentOrders: function (client) {
                this.selectedClient = client;
                // GET recentOrdersFromServer here
                // Use this.recentOrders.push(recentOrdersFromServer) to update ViewModel

            }
        },
        mounted: function () {
            // GET clientsFromServer here
            // Use this.clients.push(clientsFromServer) to update ViewModel

        }
    });
});