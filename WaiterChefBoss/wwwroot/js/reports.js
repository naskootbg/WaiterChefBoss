
window.addEventListener('load', solve);

function solve() {


    const rep = document.getElementById('reports');
    const food = document.getElementById('dvUserdetails');
    const editProduct = document.getElementById('dvProductDetails');
    const order = document.getElementById('OrderResults');
    const repDv = document.getElementById('dvReports');
    const h6 = document.getElementById('reportPeriod');

    const canvas = document.createElement('canvas');
    canvas.id = 'canvasHolder';
    const reportIt = document.getElementById('displayReport');

    reportIt.addEventListener('click', createReport);
    rep.addEventListener('click', createReport);
    
    function showReport() {
        if (canvas.textContent.length > 0) {
            repDv.removeChild(canvas);

        }


        repDv.appendChild(canvas);
        const howMnay = document.getElementById('howMany');
        const period = document.getElementById('period');
        h6.textContent = 'The report for the last ' + howMnay.value + ' ' + period.options[period.selectedIndex].textContent;
        const url = '/api/reports/full?howMany=' + howMnay.value + '&period=' + period.value;
        fetch(url).then(res => res.json()).then(vPosts);
    }
    function vPosts(responce) {
        food.style.display = 'none';
        editProduct.style.display = 'none';
        order.style.display = 'none';



        repDv.style.display = 'block';
        console.log(responce);
        let dates = [];
        let total = [];
        Object.values(responce).forEach(post => {

            dates.push(post.date);
            total.push(post.total);


        });
        // chartReport.destroy();
        if (Chart.getChart("canvasHolder")) {
            Chart.getChart("canvasHolder")?.destroy();
        }
        new Chart("canvasHolder", {
            type: "line",

            data: {
                labels: dates,
                datasets: [
                    {
                        backgroundColor: "rgba(0,0,255,1.0)",
                        borderColor: "rgba(0,0,255,0.1)",
                        label: 'Sells',
                        data: total
                    }
                ]
            },
            options: {
                responsive: true,
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: 'Date',
                            font: {
                                padding: 4,
                                size: 20,
                                weight: 'bold',
                                family: 'Arial'
                            },
                            color: 'darkblue'
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: 'Price',
                            font: {
                                size: 20,
                                weight: 'bold',
                                family: 'Arial'
                            },
                            color: 'darkblue'
                        },
                        beginAtZero: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Values',
                        }
                    }
                }
            }

        });


    }


    function createReport() {

        h6.textContent = '';
        showReport();



    };
}




