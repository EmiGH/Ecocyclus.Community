$(document).ready(function() {
	

	$(".logo-nav, .footer-logo").click(function() {
		   $('html, body').animate({scrollTop: 0,}, 1000);
	});
	$(".home").click(function() {
		   $('html, body').animate({scrollTop: 920,}, 1000);
	});	
	$("#aWhatWeDo").click(function() {
		   $('html, body').animate({scrollTop: 920,}, 1000);
	});
	$(".WhatWeDo").click(function() {
		   $('html, body').animate({scrollTop: 1810,}, 1000);
	});
	$("#aOurPrices").click(function() {
		   $('html, body').animate({scrollTop: 1810,}, 1000);
	});	
	$(".OurPrices").click(function() {
		   $('html, body').animate({scrollTop: 2500,}, 1000);
	});	
	$("#aContact, #aDemo").click(function() {
			$('html, body').animate({scrollTop: 2450,}, 1000);
	});	

	$(".contact").click(function() {
			$('html, body').animate({scrollTop: 0,}, 1000);
	});	


    if (matches = window.location.href.match(/#home/)) {
      return $('html, body').animate({scrollTop: 400,}, 1000);
    } 
    if (matches = window.location.href.match(/#WhatWeDo/)) {
      return $('html, body').animate({scrollTop: 920,}, 1000);
    } 
    if (matches = window.location.href.match(/#OurPrices/)) {
      return $('html, body').animate({scrollTop: 1810,}, 1000);
    } 
    if (matches = window.location.href.match(/#Contact/)) {
      return $('html, body').animate({scrollTop: 2450,}, 1000);
    } 	

	else if (matches = window.location.href.match()) {
      return $('html, body').animate({scrollTop: 0,}, 0);
    }

});

$(document).scroll(function () {
    
	var y = $(this).scrollTop();
    if (y > 300) {
        $('.logo-nav').fadeIn();
    } else {
        $('.logo-nav').fadeOut();
	}
	
});

$(document).scroll(function () {
    
	var y = $(this).scrollTop();
    if (y > 300) {
        $('h1').fadeIn();
    } else {
        $('h1').fadeOut();
	}

		
	
 
 
 switch (true) {

    case (y < 890):
			if(!window.location.href.match(/#home/))
				window.location.href  = "#home";
        break;
    case (y > 890 && y < 1740):
		if(!window.location.href.match(/#WhatWeDo/))
				window.location.href  = "#WhatWeDo";
        break;
    case (y > 1750 && y < 2060):
			if(!window.location.href.match(/#OurPrices/))
 				window.location.href  = "#OurPrices";
        break;
    case (y > 2170):
			if(!window.location.href.match(/#Contact/)) 			
				window.location.href  = "#Contact";
        break;		
    default:
       
        break;
}
     
});



/*
 * jQuery Watermark plugin
 *
 */

(function($)
{
    var old_ie = $.browser.msie && $.browser.version < 8;
    var hard_left = 4;
	$.watermarker = function(){};	
	$.extend($.watermarker, {
		defaults: {
			color : '#999',
			left: 0,
			top: 0,
			fallback: false,
			animDuration: 300,
			minOpacity: 0.6
		},
		setDefaults: function(settings)
		{
			$.extend( $.watermarker.defaults, settings );
		},
		checkVal: function(val, label, event_blur)
		{
			if(val.length == 0)
				$(label).show();
			else 
				$(label).hide();

			return val.length > 0;
		},
		html5_support: function() {
            var i = document.createElement('input');
            return 'placeholder' in i;
        }
	});

	$.fn.watermark = function(text, options){
		var options, elems;
		options = $.extend($.watermarker.defaults, options);
		elems = this.filter('textarea, input:not(:checkbox,:radio,:file,:submit,:reset)');

		if(options.fallback && $.watermarker.html5_support())
		    return;

		elems.each(function()
		{
			var $elem, attr_name, label_text, watermark_container, watermark_label;
			var e_margin_left, e_margin_top, pos, e_top = 0, height, line_height;

			$elem = $(this);
			if($elem.attr('data-jq-watermark') == 'processed')
			    return;

			attr_name = $elem.attr('placeholder') != undefined && $elem.attr('placeholder') != '' ? 'placeholder' : 'title';
			label_text = text === undefined || text === '' ? $(this).attr(attr_name) : text;
			watermark_container = $('<span class="watermark_container"></span>');
			watermark_label = $('<span class="watermark">' + label_text + '</span>');

			// If used, remove the placeholder attribute to prevent conflicts
 			if(attr_name == 'placeholder')
				$elem.removeAttr('placeholder');

			watermark_container.css({
				display: 'inline-block',
				position: 'relative'
			});

			if(old_ie)
			{
			    watermark_container.css({
			       zoom: 1,
			       display: 'inline' 
			    });
			}

			$elem.wrap(watermark_container).attr('data-jq-watermark', 'processed');
			if(this.nodeName.toLowerCase() == 'textarea')
			{
			    e_height = $elem.css('line-height');
				e_height = e_height === 'normal' ? parseInt($elem.css('font-size')) : e_height;
			    e_top = ($elem.css('padding-top') != 'auto' ? parseInt($elem.css('padding-top')):0);
		    }else{
		        e_height = $elem.outerHeight();
		        if(e_height <= 0)
		        {
		            e_height = ($elem.css('padding-top') != 'auto' ? parseInt($elem.css('padding-top')):0);
		            e_height += ($elem.css('padding-bottom') != 'auto' ? parseInt($elem.css('padding-bottom')):0);
		            e_height += ($elem.css('height') != 'auto' ? parseInt($elem.css('height')):0);
		        }
		    }    

		    e_top += ($elem.css('margin-top') != 'auto' ? parseInt($elem.css('margin-top')):0);

			e_margin_left = $elem.css('margin-left') != 'auto' ? parseInt($elem.css('margin-left')) : 0;
			e_margin_left += $elem.css('padding-left') != 'auto' ? parseInt($elem.css('padding-left')) : 0;

			watermark_label.css({
				position: 'absolute',
				display: 'block',
	            fontFamily: $elem.css('font-family'),
	            fontSize: $elem.css('font-size'),
	            color: options.color,
	            left: hard_left + options.left + e_margin_left,
	            top: options.top + e_top,
	            height: e_height,
	            lineHeight: e_height + 'px',
	            textAlign: 'left',
	            pointerEvents: 'none'
			}).data('jq_watermark_element', $elem);

			$.watermarker.checkVal($elem.val(), watermark_label);

			watermark_label.click(function()
            {
               $($(this).data('jq_watermark_element')).trigger('focus');
            });

			$elem.before(watermark_label)
			.bind('focus.jq_watermark', function()
			{
				if(!$.watermarker.checkVal($(this).val(), watermark_label))
    				watermark_label.stop().fadeTo(options.animDuration, options.minOpacity);
			})
			.bind('blur.jq_watermark change.jq_watermark', function()
			{
				if(!$.watermarker.checkVal($(this).val(), watermark_label))
				    watermark_label.stop().fadeTo(options.animDuration, 1);
			})
			.bind('keydown.jq_watermark', function(e)
			{
			    $(watermark_label).hide();
			})
			.bind('keyup.jq_watermark', function(e)
			{
			    $.watermarker.checkVal($(this).val(), watermark_label);
			});
		});

		return this;
	};

	$(document).ready(function()
	{
		$('.jq_watermark').watermark();
	});
})(jQuery);

