var current_G_date = new Date();
var current_J_Date = ginj(current_G_date.getFullYear(), current_G_date.getMonth() + 1, current_G_date.getDate());
var current_J_Year = current_J_Date.y;
var current_J_Month = current_J_Date.m;
var current_J_Day = current_J_Date.d;
/* time to jalali */
function t2j(date,f){
   
   var g = t2g(date,false);
   
    return ginj(g.y,g.m,g.d,f);
  
}
function t2time(date){
   
   
     var d = new Date(Date.parse(date)); 

   

 return d.toLocaleTimeString('it-IT');
  
}
/* gregorian to jalali */
function ginj(year,month,day,f){
    
    var $g_days_in_month = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31); 
  var $j_days_in_month = new Array(31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29);     
 
   $gy = year-1600; 
   $gm = month-1; 
   $gd = day-1; 

   $g_day_no = 365*$gy+div($gy+3,4)-div($gy+99,100)+div($gy+399,400); 

   for ($i=0; $i < $gm; ++$i) 
      $g_day_no += $g_days_in_month[$i]; 
   if ($gm>1 && (($gy%4==0 && $gy%100!=0) || ($gy%400==0))) 
      /* leap and after Feb */ 
      $g_day_no++; 
   $g_day_no += $gd; 

   $j_day_no = $g_day_no-79; 

   $j_np = div($j_day_no, 12053); /* 12053 = 365*33 + 32/4 */ 
   $j_day_no = $j_day_no % 12053; 

   $jy = 979+33*$j_np+4*div($j_day_no,1461); /* 1461 = 365*4 + 4/4 */ 

   $j_day_no %= 1461; 

   if ($j_day_no >= 366) { 
      $jy += div($j_day_no-1, 365); 
      $j_day_no = ($j_day_no-1)%365; 
   } 

   for ($i = 0; $i < 11 && $j_day_no >= $j_days_in_month[$i]; ++$i) 
      $j_day_no -= $j_days_in_month[$i]; 
   $jm = $i+1; 
   $jd = $j_day_no+1; 

 function div(x,y){
    return Math.floor(x/y);
    
    
}
if(!f || f==undefined)
  return {y:$jy,m:$jm,d:$jd} 
  else
      return $jy+'/'+$jm+'/'+$jd;
    
    
    
    
    
}


/* jalali to gregorian  */
function jing(year, month, day,f) 
{ 
    function div(x,y){
    return Math.floor(x/y);
    
    
}
    $g_days_in_month = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31); 
    $j_days_in_month = new Array(31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29);
    
   

   $jy = year-979; 
   $jm = month-1; 
   $jd = day-1; 

   $j_day_no = 365*$jy + div($jy, 33)*8 + div($jy%33+3, 4); 
   for ($i=0; $i < $jm; ++$i) 
      $j_day_no += $j_days_in_month[$i]; 

   $j_day_no += $jd; 

   $g_day_no = $j_day_no+79; 

   $gy = 1600 + 400*div($g_day_no, 146097); /* 146097 = 365*400 + 400/4 - 400/100 + 400/400 */ 
   $g_day_no = $g_day_no % 146097; 

   $leap = true; 
   if ($g_day_no >= 36525) /* 36525 = 365*100 + 100/4 */ 
   { 
      $g_day_no--; 
      $gy += 100*div($g_day_no,  36524); /* 36524 = 365*100 + 100/4 - 100/100 */ 
      $g_day_no = $g_day_no % 36524; 

      if ($g_day_no >= 365) 
         $g_day_no++; 
      else 
         $leap = false; 
   } 

   $gy += 4*div($g_day_no, 1461); /* 1461 = 365*4 + 4/4 */ 
   $g_day_no %= 1461; 

   if ($g_day_no >= 366) { 
      $leap = false; 

      $g_day_no--; 
      $gy += div($g_day_no, 365); 
      $g_day_no = $g_day_no % 365; 
   } 

   for ($i = 0; $g_day_no >= $g_days_in_month[$i] + ($i == 1 && $leap); $i++) 
      $g_day_no -= $g_days_in_month[$i] + ($i == 1 && $leap); 
   $gm = $i+1; 
   $gd = $g_day_no+1; 

if(!f || f==undefined)
  return {y:$gy,m:$gm,d:$gd} 
  else
      return $gy+'/'+$gm+'/'+$gd;
}

/* time to gregorian  */
function t2g(date,f){
    
    

   var d = new Date(Date.parse(date)); 
   var day = d.getDate();
   var month = d.getMonth() +1;
   var year = d.getFullYear(); 
      
  if(!f || f==undefined)
  return {y:year,m:month,d:day} 
  else
      return year+'/'+month+'/'+day;  
    
    
}
//by persian-date.js
function daysInMonth(yearInput, monthInput,persian_or_gregorian) {
	var year = yearInput ? yearInput : current_J_Year,
		month = monthInput ? monthInput : current_J_Month;
	if (persian_or_gregorian === 'persian') {
		if (month < 1 || month > 12) return 0;
		if (month < 7) return 31;
		if (month < 12) return 30;
		if (leap_persian(year)) {
			return 30;
		}
		return 29;
	}
	if (persian_or_gregorian === 'gregorian') {
		return new Date(year, month, 0).getDate();
	}
}
function leap_persian(year) {
	return ((year - (year > 0 ? 474 : 473)) % 2820 + 474 + 38) * 682 % 2816 < 682;
}
//function gregorian_to_jalali(gy,gm,gd){
//	var g_d_m=[0,31,59,90,120,151,181,212,243,273,304,334];
//	var jy=(gy<=1600)?0:979;
//	gy-=(gy<=1600)?621:1600;
//	var gy2=(gm>2)?(gy+1):gy;
//	var days=(365*gy) +(parseInt((gy2+3)/4)) -(parseInt((gy2+99)/100))
//		+(parseInt((gy2+399)/400)) -80 +gd +g_d_m[gm-1];
//	jy+=33*(parseInt(days/12053));
//	days%=12053;
//	jy+=4*(parseInt(days/1461));
//	days%=1461;
//	jy+=parseInt((days-1)/365);
//	if(days > 365)days=(days-1)%365;
//	var jm=(days < 186)?1+parseInt(days/31):7+parseInt((days-186)/30);
//	var jd=1+((days < 186)?(days%31):((days-186)%30));
//	return [jy,jm,jd];
//}
//function jalali_to_gregorian(jy,jm,jd){
//	var gy=(jy<=979)?621:1600;
//	jy-=(jy<=979)?0:979;
//	var days=(365*jy) +((parseInt(jy/33))*8) +(parseInt(((jy%33)+3)/4))
//		+78 +jd +((jm<7)?(jm-1)*31:((jm-7)*30)+186);
//	gy+=400*(parseInt(days/146097));
//	days%=146097;
//	if(days > 36524){
//		gy+=100*(parseInt(--days/36524));
//		days%=36524;
//		if(days >= 365)days++;
//	}
//	gy+=4*(parseInt((days)/1461));
//	days%=1461;
//	gy+=parseInt((days-1)/365);
//	if(days > 365)days=(days-1)%365;
//	var gd=days+1;
//	var sal_a=[0,31,((gy%4==0 && gy%100!=0) || (gy%400==0))?29:28,31,30,31,30,31,31,30,31,30,31];
//	var gm
//	for(gm=0;gm<13;gm++){
//		var v=sal_a[gm];
//		if(gd <= v)break;
//		gd-=v;
//	}
//	return [gy,gm,gd];
//}