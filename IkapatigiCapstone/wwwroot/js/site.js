// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

////for homeoption slides
//$(function () {
//	// Slideshow 4
//	$("#slider4").responsiveSlides({
//		auto: true,
//		pager: true,
//		nav: false,
//		speed: 900,
//		namespace: "callbacks",
//		before: function () {
//			$('.events').append("<li>before event fired.</li>");
//		},
//		after: function () {
//			$('.events').append("<li>after event fired.</li>");
//		}

//	});

//});

////bootstrap
//$.fn.emulateTransitionEnd = function (duration) {
//	var called = false
//	var $el = this
//	$(this).one('bsTransitionEnd', function () { called = true })
//	var callback = function () { if (!called) $($el).trigger($.support.transition.end) }
//	setTimeout(callback, duration)
//	return this
//}

//// http://blog.alexmaccaw.com/css-transitions
//$.fn.emulateTransitionEnd = function (duration) {
//	var called = false
//	var $el = this
//	$(this).one('bsTransitionEnd', function () { called = true })
//	var callback = function () { if (!called) $($el).trigger($.support.transition.end) }
//	setTimeout(callback, duration)
//	return this
//}

//Modal.prototype.backdrop = function (callback) {
//    var that = this
//    var animate = this.$element.hasClass('fade') ? 'fade' : ''

//    if (this.isShown && this.options.backdrop) {
//        var doAnimate = $.support.transition && animate

//        this.$backdrop = $(document.createElement('div'))
//            .addClass('modal-backdrop ' + animate)
//            .appendTo(this.$body)

//        this.$element.on('click.dismiss.bs.modal', $.proxy(function (e) {
//            if (this.ignoreBackdropClick) {
//                this.ignoreBackdropClick = false
//                return
//            }
//            if (e.target !== e.currentTarget) return
//            this.options.backdrop == 'static'
//                ? this.$element[0].focus()
//                : this.hide()
//        }, this))

//        if (doAnimate) this.$backdrop[0].offsetWidth // force reflow

//        this.$backdrop.addClass('in')

//        if (!callback) return

//        doAnimate ?
//            this.$backdrop
//                .one('bsTransitionEnd', callback)
//                .emulateTransitionEnd(Modal.BACKDROP_TRANSITION_DURATION) :
//            callback()

//    } else if (!this.isShown && this.$backdrop) {
//        this.$backdrop.removeClass('in')

//        var callbackRemove = function () {
//            that.removeBackdrop()
//            callback && callback()
//        }
//        $.support.transition && this.$element.hasClass('fade') ?
//            this.$backdrop
//                .one('bsTransitionEnd', callbackRemove)
//                .emulateTransitionEnd(Modal.BACKDROP_TRANSITION_DURATION) :
//            callbackRemove()

//    } else if (callback) {
//        callback()
//    }
//}
///// another section for callback
//Modal.prototype.backdrop = function (callback) {
//    var that = this
//    var animate = this.$element.hasClass('fade') ? 'fade' : ''

//    if (this.isShown && this.options.backdrop) {
//        var doAnimate = $.support.transition && animate

//        this.$backdrop = $(document.createElement('div'))
//            .addClass('modal-backdrop ' + animate)
//            .appendTo(this.$body)

//        this.$element.on('click.dismiss.bs.modal', $.proxy(function (e) {
//            if (this.ignoreBackdropClick) {
//                this.ignoreBackdropClick = false
//                return
//            }
//            if (e.target !== e.currentTarget) return
//            this.options.backdrop == 'static'
//                ? this.$element[0].focus()
//                : this.hide()
//        }, this))

//        if (doAnimate) this.$backdrop[0].offsetWidth // force reflow

//        this.$backdrop.addClass('in')

//        if (!callback) return

//        doAnimate ?
//            this.$backdrop
//                .one('bsTransitionEnd', callback)
//                .emulateTransitionEnd(Modal.BACKDROP_TRANSITION_DURATION) :
//            callback()

//    } else if (!this.isShown && this.$backdrop) {
//        this.$backdrop.removeClass('in')

//        var callbackRemove = function () {
//            that.removeBackdrop()
//            callback && callback()
//        }
//        $.support.transition && this.$element.hasClass('fade') ?
//            this.$backdrop
//                .one('bsTransitionEnd', callbackRemove)
//                .emulateTransitionEnd(Modal.BACKDROP_TRANSITION_DURATION) :
//            callbackRemove()

//    } else if (callback) {
//        callback()
//    }
//}

////another callback section (2)
//Tooltip.prototype.hide = function (callback) {
//    var that = this
//    var $tip = $(this.$tip)
//    var e = $.Event('hide.bs.' + this.type)

//    function complete() {
//        if (that.hoverState != 'in') $tip.detach()
//        if (that.$element) { // TODO: Check whether guarding this code with this `if` is really necessary.
//            that.$element
//                .removeAttr('aria-describedby')
//                .trigger('hidden.bs.' + that.type)
//        }
//        callback && callback()
//    }

//    this.$element.trigger(e)

//    if (e.isDefaultPrevented()) return

//    $tip.removeClass('in')

//    $.support.transition && $tip.hasClass('fade') ?
//        $tip
//            .one('bsTransitionEnd', complete)
//            .emulateTransitionEnd(Tooltip.TRANSITION_DURATION) :
//        complete()

//    this.hoverState = null

//    return this
//}
////another callback section (3)

//Tab.prototype.activate = function (element, container, callback) {
//    var $active = container.find('> .active')
//    var transition = callback
//        && $.support.transition
//        && ($active.length && $active.hasClass('fade') || !!container.find('> .fade').length)

//    function next() {
//        $active
//            .removeClass('active')
//            .find('> .dropdown-menu > .active')
//            .removeClass('active')
//            .end()
//            .find('[data-toggle="tab"]')
//            .attr('aria-expanded', false)

//        element
//            .addClass('active')
//            .find('[data-toggle="tab"]')
//            .attr('aria-expanded', true)

//        if (transition) {
//            element[0].offsetWidth // reflow for transition
//            element.addClass('in')
//        } else {
//            element.removeClass('fade')
//        }

//        if (element.parent('.dropdown-menu').length) {
//            element
//                .closest('li.dropdown')
//                .addClass('active')
//                .end()
//                .find('[data-toggle="tab"]')
//                .attr('aria-expanded', true)
//        }

//        callback && callback()
//    }

//    $active.length && transition ?
//        $active
//            .one('bsTransitionEnd', next)
//            .emulateTransitionEnd(Tab.TRANSITION_DURATION) :
//        next()

//    $active.removeClass('in')
//}

