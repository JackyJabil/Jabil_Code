﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OEE_DASHBOARD.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <title>OEE DASHBOARD</title>
    <link href="type_mainPage.css" rel="stylesheet" />
    <link href="jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="jquery.js"></script>
    <script type="text/javascript" src="echarts-all.js"></script> 
    <script src="jquery-ui.js"></script>
    <script type="text/javascript">
        var opendialog = 0;

        var IEwidth = $(window).width();
        var Cellwidth = "140";
        $(document).ready(function () {
            message1();
            setInterval("message1()", 1800000);
        });

        function message1() {
            $.getJSON('json_hua.txt', function (data) {
                for (var i = 0; i < data.OEEData.length; i++) {
                    var Ma = data.OEEData[i].Machine
                    var idchart = "idchart" + i;
                    var idbar = "idbara" + i;
                    var idbarrange = "idbarrangeb" + i;
                    var idbar1 = "idbarc" + i;
                    var idbarrange1 = "idbarranged" + i;
                    var idbar2 = "idbare" + i;
                    var idbarrange2 = "idbarrangef" + i;

                    //颜色取值范围
                    //OEE
                    var OEE1 = 0, OEE2 = 0;

                    switch (Ma) {
                        case "MC0110": Ma = "MC0110", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MC0218": Ma = "MC0218", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MC0555": Ma = "MC0555", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MC0569": Ma = "MC0569", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MC0680": Ma = "MC0680", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MC0833": Ma = "MC0833", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MC8318": Ma = "MC8318", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MC8811": Ma = "MC8811", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MC8835": Ma = "MC8835", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MCA057": Ma = "MCA057", OEE1 = 0.40, OEE2 = 0.5; break;
                        case "MCA697": Ma = "MCA697", OEE1 = 0.40, OEE2 = 0.5; break;
                        default: break;

                    }
                    if ((data.OEEData[i].OEE * 100).toFixed(0) == 0) {
                        data.OEEData[i].Availability = 0;
                        data.OEEData[i].Performance = 0;
                        data.OEEData[i].Quality = 0;
                    }
                    if (data.OEEData[i].Performance>0.98) {
                        data.OEEData[i].Performance =0.98;
                    }
                    //document.getElementById("d" + i).innerHTML = idchart;
                    //document.write(Ma+","+data.OEEData[i].OEE + "," + data.OEEData[i].Availability + "," + data.OEEData[i].PPM + "<br>");


                    //if (i == 6 || i == 8) {
                    //    document.getElementById("d" + i).innerHTML = "<div class='cent'><table></table></div>";

                    //    continue;
                    //}

                    document.getElementById("d" + i).innerHTML = "<div class='cent'><table><th colspan='2'>" + Ma + "</th><tr><td rowspan='3'><div class='chart' id='" + idchart + "'></div></td><td>Availability:<div id='" + idbar + "'><div id='" + idbarrange + "'style='width:" + (data.OEEData[i].Availability * 100).toFixed(0) + "%;'></div><span>" + (data.OEEData[i].Availability * 100).toFixed(0) + "%</span></div</td></tr><tr><td>Performance:<div id='" + idbar1 + "'><div id='" + idbarrange1 + "'style='width:" + (data.OEEData[i].Performance * 100).toFixed(0) + "%;'></div><span>" + (data.OEEData[i].Performance * 100).toFixed(0) + "%</span></div</td></tr><tr><td>Quality:<div id='" + idbar2 + "'><div id='" + idbarrange2 + "'style='width:" + (data.OEEData[i].Quality * 100).toFixed(0) + "%;'></div><span>" + (data.OEEData[i].Quality * 100).toFixed(0) + "%</span></div</td></tr></table></div>";

                   
                    $('#d' + i).bind("dblclick.a", function (event) { //双击事件                       
                       
                        var x = event.pageX;
                        var y = event.pageY;
                        var x_text = (x + 275) + "px";
                        var y_text = (y + 175) + "px";
                        if (opendialog == 0) {
                            opendialog = 1;
                            var options = { title: "GetmachineOEE", width: 550, height: 350}
                            var html = '<div class="dialog1" id="dialog-window" title="提示信息">' +
                            ' <iframe src="' + "ShowmachineOEE.html?id=" + this.id + '" frameBorder="0" style="border: 0; " scrolling="NO" width="100%" height="450"></iframe>' +
                            '</div>';
                            $(html).dialog($.extend({
                                modal: true,
                                closeOnEscape: false,
                                draggable: true,
                                resizable: true,
                                position: { my: "center", at: "left+" + x_text + " top+" + y_text + " ", of: window },
                                open: function (event, ui) {                                            
                                },                   
                                close: function (event, ui) {
                                    opendialog = 0;
                                    $(this).dialog("destroy"); // 关闭时销毁
                                }
                            }, options));

                          
                        }
                     
                    });


                    //颜色取值范围         
                    var Availability1 = 50, Availability2 = 60;
                    var Performance1 = 60, Performance2 = 65;
                    var Quality1 = 96, Quality2 = 99;
                    //颜色取值范围

                    //Availability
                    if (parseFloat((data.OEEData[i].Availability * 100)) < Availability1) {
                        $("#" + idbar).css({ "width": Cellwidth + "px", "border": "1px solid #e74c3c", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange).css({ "height": "16px", "background": "#e74c3c", "float": "left" });//红色
                    }
                    else if (parseFloat((data.OEEData[i].Availability * 100)) > Availability2) {
                        $("#" + idbar).css({ "width": Cellwidth + "px", "border": "1px solid #2ecc71", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange).css({ "height": "16px", "background": "#2ecc71", "float": "left" });//绿色
                    }
                    else {
                        $("#" + idbar).css({ "width": Cellwidth + "px", "border": "1px solid #ffc300", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange).css({ "height": "16px", "background": "#ffc300", "float": "left" });//黄色               
                    }
                    //Availability

                    //Performance
                   
                    if (parseFloat((data.OEEData[i].Performance * 100)) < Performance1) {
                        $("#" + idbar1).css({ "width": Cellwidth + "px", "border": "1px solid #e74c3c", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange1).css({ "height": "16px", "background": "#e74c3c", "float": "left" });//红色
                    }
                    else if (parseFloat((data.OEEData[i].Performance * 100)) > Performance2) {
                        $("#" + idbar1).css({ "width": Cellwidth + "px", "border": "1px solid #2ecc71", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange1).css({ "height": "16px", "background": "#2ecc71", "float": "left" });//绿色
                    }
                    else {
                        $("#" + idbar1).css({ "width": Cellwidth + "px", "border": "1px solid #ffc300", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange1).css({ "height": "16px", "background": "#ffc300", "float": "left" });//黄色
                    }
                    //Performance


                    //Quality
                   

                    if (parseFloat((data.OEEData[i].Quality * 100)) < Quality1) {
                        $("#" + idbar2).css({ "width": Cellwidth + "px", "border": "1px solid #e74c3c", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange2).css({ "height": "16px", "background": "#e74c3c", "float": "left" });//红色
                    }
                    else if (parseFloat((data.OEEData[i].Quality * 100)) > Quality2) {
                        $("#" + idbar2).css({ "width": Cellwidth + "px", "border": "1px solid #2ecc71", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange2).css({ "height": "16px", "background": "#2ecc71", "float": "left" });//绿色
                    }
                    else {
                        $("#" + idbar2).css({ "width": Cellwidth + "px", "border": "1px solid #ffc300", "box-shadow": "#333 0px 0px 0px", "height": "16px", "position": "relative" });
                        $("#" + idbarrange2).css({ "height": "16px", "background": "#ffc300", "float": "left" });//黄色
                    }
                    //Quality

                  
                    var color1, color2, color3;
                    if ((data.OEEData[i].OEE * 100).toFixed(0) == 0) {
                        color1 = '#c1c1c1', color2 = '#c1c1c1', color3 = '#c1c1c1';
                        $("#" + idbar).css({ "border": "1px solid #c1c1c1" });
                        $("#" + idbar1).css({ "border": "1px solid #c1c1c1" });
                        $("#" + idbar2).css({ "border": "1px solid #c1c1c1" });
                    }
                    else { color1 = '#e74c3c', color2 = '#ffc300', color3 = '#2ecc71'; }

                    //chart
                    var myChart = echarts.init(document.getElementById(idchart));                   
                    
                    var option = {
                        tooltip: {
                            show: true,
                            position: ['50%', '50%'],
                            formatter: "{a} <br/>{b} : {c}%"

                        },
                        toolbox: {
                            show: false,
                            feature: {
                                mark: { show: false },
                                restore: { show: false },
                                saveAsImage: { show: false }
                            }
                        },
                        series: [
                            {
                                name: Ma,
                                type: 'gauge',
                                min: 0,
                                max: 100,
                                splitNumber: 10,       // 分割段数，默认为5
                                axisLine: {            // 坐标轴线
                                    lineStyle: {       // 属性lineStyle控制线条样式
                                        color: [[OEE1, color1], [OEE2, color2], [1, color3]],
                                        width: 8
                                    }
                                },
                                axisTick: {            // 坐标轴小标记
                                    show: true,
                                    splitNumber: 10,   // 每份split细分多少段
                                    length: 12,        // 属性length控制线长
                                    lineStyle: {       // 属性lineStyle控制线条样式
                                        color: 'auto'
                                    }
                                },
                                axisLabel: {           // 坐标轴文本标签，详见axis.axisLabel
                                    show: false,
                                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                                        color: 'auto'
                                    }
                                },
                                splitLine: {           // 分隔线
                                    show: true,        // 默认显示，属性show控制显示与否
                                    length: 30,         // 属性length控制线长
                                    lineStyle: {       // 属性lineStyle（详见lineStyle）控制线条样式
                                        color: 'auto'
                                    }
                                },
                                pointer: {
                                    width: 5
                                },
                                title: {
                                    show: true,
                                    offsetCenter: [0, '-40%'],       // x, y，单位px
                                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                                        fontWeight: 'bolder',
                                        fontSize: 10
                                    }
                                  
                                },
                                detail: {
                                    formatter: '{value}%',
                                    textStyle: {       // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                                        color: 'auto',
                                        fontWeight: 'bolder',
                                        fontSize: 25


                                    }
                                },
                                data: [{ value: (data.OEEData[i].OEE * 100).toFixed(0), name: 'UTILIZATION' }]
                            }
                        ]

                    };

                    // 为echarts对象加载数据 
                    myChart.setOption(option);

                    //setInterval(function () {
                    //    option.series[0].data[0].value = (data.OEEData[i].OEE * 100).toFixed(0) - 0;
                    //    myChart.setOption(option, true);
                    //}, 2000);




                }//for
            }
           );
        }
    </script>
</head>
<body>
    <form id="form1">      
        <div class="parent">
            <div id="d0"></div>
            <div id="d1"></div>
            <div id="d2"></div>
            <div id="d3"></div>
            <div id="d4"></div>
            <div id="d5"></div>
            <div id="d6"></div>
            <div id="d7"></div>
            <div id="d8"></div>
            <div id="d9"></div>
            <div id="d10"></div>
            <div id="d11"></div>
        </div>  
    </form>
   <div style="position:absolute;width:200px;height:150px;bottom:0;left:50%;margin-left:-100px;"><a href="javascript:window.location.reload(); ">Refresh</a></div>
   
</body>
</html>
