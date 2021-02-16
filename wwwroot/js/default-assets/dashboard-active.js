function DrawDashboardCharts() {
    var options = {
        chart: {
            height: 330,
            type: "bar",
            stacked: !0,
            toolbar: {
                show: !1
            },
            zoom: {
                enabled: !0
            }
        },
        plotOptions: {
            bar: {
                horizontal: !1,
                columnWidth: "15%",
                endingShape: "rounded"
            }
        },
        dataLabels: {
            enabled: !1
        },
        series: [{
            data: [178, 155, 115, 185, 148, 196, 180, 181, 150, 140, 180, 160]
        }],
        xaxis: {
            categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
        },
        colors: ["#727cf5"],
        legend: {
            position: "bottom"
        },
        fill: {
            opacity: 1
        }
    };
    (chart = new ApexCharts(document.querySelector("#apex-stacked-bar-chart"), options)).render();

    var options = {
        chart: {
            height: 250,
            type: "donut"
        },
        stroke: {
            colors: ['rgba(0,0,0,0)']
        },
        colors: ["#6610f2", "#0168fa", "#0acf97", "#fbbc06"],
        legend: {
            position: 'top',
            horizontalAlign: 'center'
        },
        dataLabels: {
            enabled: false
        },
        series: [56, 48, 42, 33],
        labels: ["Smartphons 56k", "Clothes 48k", "Electronics 42k", "Other 33k"]
    };

    var chart = new ApexCharts(document.querySelector("#apexdonutchart"), options);

    chart.render();
}