let showFirst = false,loadFormer = false,oneTranslation = false;
let vocabulary = {};
let rangeStart = 0,rangeEnd = 0,pageStart = 0,pageEnd = 0;
let sindex = 0,eindex = 0;
const vocabularies={
    "mid_vocab": "中考考纲词汇/词组"
};
let current;

($(()=>{
    getVocabularies();
    $(".exercise").hide();

    $("#start").click(()=>{
        showFirst = $("#showfirst").prop("checked");
        loadFormer = $("#loadformer").prop("checked");
        oneTranslation = $("#onetranslation").prop("checked");
        rangeStart = $("#from").val();
        rangeEnd = $("#to").val();
        pageStart = parseInt($("#pgfrom").val());
        pageEnd = parseInt($("#pgto").val());

        console.log(showFirst,loadFormer);
        $.get(`../../data/vocabulary/${$("#vocabulary").val()}.json`,(data,status) => {
            if(status=="success"){
                vocabulary = data;
                initializeExercise();
            }
            else console.log(`Error ${status}`);
        });

        $(".settings").hide();        
    })
}))();


function getVocabularies(){
    let sel = $("#vocabulary");

    for(let i in vocabularies){
        sel.append($("<option></option>").val(i).text(vocabularies[i]));
    }
}

function initializeExercise(){
    $(".exercise").show();
    $("#vocabname").text(vocabulary.name);
    $("#check").click(check);
    $("#next").click(generate);
    $("#end").click(endTraining);
    $("#trans").keyup((e)=>{
        if(e.keyCode==0x0D){
            check();
        }
    });
    extractPages();
    getPosition();
    generate();
}

function generate(){
    $(".correct").hide();
    $(".wrong").hide();
    $(".operations").hide();
    $("#trans").val("");
    $("#trans").unbind("keyup",nextEvent)
    current = vocabulary.vocabulary[Math.floor(Math.random()*(eindex-sindex+1)+sindex)];
    let meaning = current.meanings[Math.floor(Math.random()*current.meanings.length)];
    let translation = meaning.translations[Math.floor(Math.random()*meaning.translations.length)];
    $(".question").text(`${meaning.type}. ${oneTranslation?translation:meaning.translations.join("；")}`);
    if(showFirst) $("#firstletter").text(current.word[0]);
}

function check(){
    $(".operations").show();

    $("#trans").bind("keyup",nextEvent);

    let answer = $("#trans").val();
    if(showFirst) answer = current.word[0] + answer;

    if(answer==current.word){
        correct();
        return;
    }
    if(current.phonic_changes){
        if(current.phonic_changes.includes(answer)){
            correct();
            return;
        }
    }
    wrong();
}

function correct(){
    $(".correct").show();
}

function wrong(){
    $("#correctanswer").text(current.word);
    $(".wrong").show();
}

function nextEvent(e){
    if(e.keyCode==0x27){
        generate();
    }
}

function extractPages(){
    pageStart = pageStart || 0;
    pageEnd = pageEnd || Infinity;

    vocabulary.vocabulary = vocabulary.vocabulary.filter((el) => el.paging >= pageStart && el.paging <= pageEnd);
}

function getPosition(){
    if(rangeStart!=null&&rangeStart!=""&&rangeEnd!=null&&rangeEnd!=""){
        let l=0,r=vocabulary.vocabulary.length-1,mid;
        while(l<r){
            mid=(l+r)>>1;
            if(rangeStart<=vocabulary.vocabulary[mid].word)r=mid;
            else l=mid+1;
        }
        sindex = r;

        l=0;r=vocabulary.vocabulary.length-1;

        while(l<r){
            mid=(l+r+1)>>1;
            if(rangeEnd>=vocabulary.vocabulary[mid].word)l=mid;
            else r=mid-1;
        }
        eindex = l;
    }
    else{
        sindex = 0;eindex = vocabulary.vocabulary.length-1;
    }
}

function endTraining(){
    $(".exercise").hide();
    $("#vocabname").text(vocabulary.name);
    $("#check").unbind();
    $("#next").unbind();
    $("#trans").unbind();
    $("#trans").unbind();
    $("#end").unbind();
    $(".settings").show();
}