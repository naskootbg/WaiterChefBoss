
window.addEventListener('load', solve);

function solve() {

    const url = '/api/reports/full';
    const rep = document.getElementById('reports');
    const food = document.getElementById('EditFood');
    const editProduct = document.getElementById('EditProduct');
    const order = document.getElementById('OrderClick');
    const repDv = document.getElementById('dvReports');

    rep.addEventListener('click', createReport);
    let dates = [];
    let total = [];
    function showReport() {
        fetch(url).then(res => res.json()).then(vPosts);
    }
    function vPosts(responce) {
        food.style.display = 'none';
        editProduct.style.display = 'none';
        order.style.display = 'none';

        repDv.style.display = 'block';
        console.log(responce);
        Object.values(responce).forEach(post => {

            dates.push(post.date);
            total.push(post.total);


        });
        new Chart("dvReports", {
            type: "line",
            
            data: {
                labels: dates,
                datasets: [{
                    backgroundColor: "rgba(0,0,255,1.0)",
                    borderColor: "rgba(0,0,255,0.1)",
                    label: 'Sells',
                    data: total
                }]
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
        // alert('I will display reports');
        showReport();



    };
}




