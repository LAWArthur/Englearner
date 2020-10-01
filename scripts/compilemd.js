($(()=>{
    
    $(".md").each((i,el)=>{
        console.log(el);
        $(el).html(marked($(el).html()));
    });
}))();